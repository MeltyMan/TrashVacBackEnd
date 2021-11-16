using System;
using System.Collections.Generic;
using System.Text;
using TrashVacBackEnd.Core.Entity;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace TrashVacBackEnd.Core.Repository
{
    public class SqlUserRepository : IUserRepository
    {
        #region Public Methods
        
        public IList<User> GetUserList()
        {
            IList<User> userList = new List<User>();

            var connectionString = ServiceProvider.Current.Configuration.ConnectionStrings.TrashVacDbConnectionString;


            SqlConnection connection =
                new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("dbo.sUsers_List", connection)
                { CommandType = CommandType.StoredProcedure };
            
            
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                userList.Add(new User()
                {
                    Id = dr.GetGuid(0), FirstName = dr.GetString(1), LastName = dr.GetString(2),
                    UserLevel = (Enums.UserLevel)dr.GetInt32(3)
                });
            }

            dr.Close();
            dr = null;

            connection.Close();
            


            return userList;

        }

        public Guid CreateUser(UserFull user)
        {


            var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
                .TrashVacDbConnectionString);
            conn.Open();

            var pwdHash = Common.GenerateMd5Hash(user.Pwd);

            var cmd = new SqlCommand("dbo.sUser_Create", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 255) { Value = user.UserName });
            cmd.Parameters.Add(new SqlParameter("@PwdHash", SqlDbType.NVarChar, 255) { Value = pwdHash });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = user.FirstName });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = user.LastName });
            cmd.Parameters.Add(new SqlParameter("@UserLevel", SqlDbType.Int) { Value = (int)user.UserLevel });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
                { Value = null, Direction = ParameterDirection.Output });

            cmd.ExecuteNonQuery();

            var userId = new Guid(cmd.Parameters[5].Value.ToString());

            conn.Close();

            return userId;

        }

        public bool TryLogin(string userName, string password, out UserAuthenticated user)
        {
            var result = false;

            user = null;
            var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
                .TrashVacDbConnectionString);
            conn.Open();
            var cmd = new SqlCommand("dbo.sUser_GetByUserName", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 255) { Value = userName });
            cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
                { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@PwdHash", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@UserLevel", SqlDbType.Int)  { Direction = ParameterDirection.Output });

            cmd.ExecuteNonQuery();

            if (Convert.ToBoolean(cmd.Parameters[1].Value))
            {
                var dbPwdHash = cmd.Parameters[3].Value.ToString();
                var pwdHash = Common.GenerateMd5Hash(password);

                if (dbPwdHash.Equals(pwdHash, StringComparison.Ordinal))
                {
                    result = true;
                    user = new UserAuthenticated()
                    {
                        Id = new Guid(cmd.Parameters[2].Value.ToString()),
                        UserName = userName,
                        FirstName = cmd.Parameters[4].Value.ToString(),
                        LastName = cmd.Parameters[5].Value.ToString(),
                        UserLevel = (Enums.UserLevel)Convert.ToInt32(cmd.Parameters[6].Value), 
                        AccessToken = GenerateAccessToken()
                    };
                    UpdateAccessToken(user.Id, user.AccessToken);

                }
            }

            conn.Close();
            
            return result;
        }

        public bool ValidateToken(string token, out UserAuthenticated user)
        {
            var result = false;
            user = null;

            var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
                .TrashVacDbConnectionString);
            conn.Open();
            var cmd = new SqlCommand("dbo.sUser_ValidateToken", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token });
            cmd.Parameters.Add(new SqlParameter("@TTL", SqlDbType.Int)
                { Value = ServiceProvider.Current.Configuration.SiteSettings.AccessTokenTtl });
            cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
                { Direction = ParameterDirection.Output });


            cmd.ExecuteNonQuery();

            result = Convert.ToBoolean(cmd.Parameters[2].Value);

            if (result)
            {
                user = new UserAuthenticated() { Id = new Guid(cmd.Parameters[3].Value.ToString()) };
            }

            conn.Close();

            return result;
        }
        #endregion
        
        #region Private Methods
        
        
        private string GenerateAccessToken()
        {
            const string VALID_CHARS = "0123456789abcdefghijklmnopqrstuvwx";

            var token = string.Empty;
            var done = false;
            var random = new Random();

            while (!done)
            {
                token = string.Empty;
                for (var i = 0; i < 50; i++)
                {
                    var index = random.Next(0, VALID_CHARS.Length - 1);

                    token = $"{token}{VALID_CHARS.Substring(index, 1)}";
                    
                }
                done = IsTokenUnique(token);
            }


            return token;


        }

        private bool IsTokenUnique(string token)
        {
            var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
                .TrashVacDbConnectionString);
            conn.Open();
            var cmd = new SqlCommand("dbo.sUser_IsTokenUnique", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token });
            cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output });

            cmd.ExecuteNonQuery();
            var result = Convert.ToBoolean(cmd.Parameters[1].Value);
            conn.Close();
            return result;
        }

        private void UpdateAccessToken(Guid userId, string token)
        {
            var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
                .TrashVacDbConnectionString);
            conn.Open();

            var cmd = new SqlCommand("[dbo].[sUser_PersistToken]", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token });
            cmd.Parameters.Add(new SqlParameter("@TTL", SqlDbType.Int)
                { Value = ServiceProvider.Current.Configuration.SiteSettings.AccessTokenTtl });

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        #endregion
    }
}
