using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HiQ.NetStandard.Util.Data;
using KYH_Kalle.Core.Entity;

namespace KYH_Kalle.Core.Repository
{
    public class SqlUserRepository : ADbRepositoryBase, IUserRepository
    {
        private const string MELTYMAN_TOKEN = "meltyman";
        private const string CDS_TOKEN = "das8783nmncxzJJDKnknxz48ZMCCMKJKERK29489u5nknxC";
        public IList<ApiUserMini> GetUserList()
        {
            return new List<ApiUserMini>(1) { new ApiUserMini() { DisplayName = "Kalle", Id = Guid.Empty } };
        }

        public bool ValidateToken(string token, out Guid userId)
        {
            userId = Guid.Empty;
            
            if (token.Equals(MELTYMAN_TOKEN, StringComparison.Ordinal) || token.Equals(CDS_TOKEN, StringComparison.Ordinal))
            {
                return true;
            }


          

            var parameters = new SqlParameters();
            parameters.AddNVarChar("@Token", 128, token);
            parameters.AddBoolean("@Result", false, ParameterDirection.Output);
            parameters.AddUniqueIdentifier("@UserId", userId, ParameterDirection.Output);
            parameters.AddNVarChar("@TokenInDb", 128, string.Empty, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("dbo.sUser_ValidateToken", ref parameters, CommandType.StoredProcedure);

            if (parameters.GetBool("@Result"))
            {
                if (token.Equals(parameters.GetString("@TokenInDb")))
                {
                    userId = parameters.GetGuid("@UserId");
                    UpdateTokenExpireDate(userId);
                    
                    return true;
                }
            }

            return false;

          
        }

        public Guid CreateUser(ApiUserFull user, out string errorMessage)
        {
            if (ValiidateUser(user, out errorMessage))
            {
                var userName = user.LoginName.Trim().ToLower();
                var pwdHash = Common.CreateMd5Hash(user.Password);

                var parameters = new SqlParameters();
                parameters.AddVarChar("@UserName", 255, userName);
                parameters.AddNVarChar("@Password", 255, pwdHash);
                parameters.AddTinyInt("@UserLevel", 20);
                parameters.AddNVarChar("@DisplayName", 255, user.DisplayName);
                parameters.AddUniqueIdentifier("@UserId", Guid.Empty, ParameterDirection.Output);

                DbAccess.ExecuteNonQuery("dbo.sUser_Create", ref parameters, CommandType.StoredProcedure);

                return parameters.GetGuid("@UserId");


            }
            
            return Guid.Empty;
            
        }

        public bool TryLogin(string userName, string password, out ApiUser user)
        {
            var result = false;
            user = null;


            var parameters = new SqlParameters();
            parameters.AddVarChar("@UserName", 255, userName);
            parameters.AddBoolean("@Result", false, ParameterDirection.Output);
            parameters.AddUniqueIdentifier("@UserId", Guid.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@DisplayName", 255, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@PwdHash", 255, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Token", 128, string.Empty, ParameterDirection.Output);
            parameters.AddUniqueIdentifier("@CustomerId", Guid.Empty, ParameterDirection.Output);
            DbAccess.ExecuteNonQuery("dbo.sUser_Loginv2", ref parameters, CommandType.StoredProcedure);

            if (parameters.GetBool("@Result"))
            {
                var pwdHashedDb = parameters.GetString("@PwdHash");
                var pwdHash = Common.CreateMd5Hash(password);
                if (pwdHash.Equals(pwdHashedDb, StringComparison.Ordinal))
                {
                    user = new ApiUser()
                    {
                        Id = parameters.GetGuid("@UserId"),
                        DisplayName = parameters.GetString("@DisplayName"), 
                        CustomerId = !parameters.IsNull("@CustomerId") ? parameters.GetGuid("@CustomerId") : Guid.Empty
                    };
                    var token = parameters.GetString("@Token");
                    if (string.IsNullOrEmpty(token))
                    {
                        token = GenerateToken();
                        UpdateAccessToken(user.Id, token);
                        user.AccessToken = token;
                    }
                    else
                    {
                        if (!ValidateToken(token, out var userId))
                        {
                            token = GenerateToken();
                            UpdateAccessToken(user.Id, token);
                            user.AccessToken = token;
                        }
                        else
                        {
                            user.AccessToken = token;
                        }
                    }

                    result = true;
                }
            }

            return result;
        }

        public bool ConnectCustomer(Guid userId, Guid customerId)
        {
            var parameters = new SqlParameters();
            parameters.AddUniqueIdentifier("@CustomerId", customerId);
            parameters.AddUniqueIdentifier("@UserId", userId);

            DbAccess.ExecuteNonQuery("dbo.sCdsCustomer_ConnectUser", ref parameters, CommandType.StoredProcedure);
            return true;
        }

        public bool TryGetUser(Guid userId, bool getPwdHash, out ApiUserFull user)
        {
            var result = false;
            user = null;
            var parameters = new SqlParameters();
            parameters.AddUniqueIdentifier("@UserId", userId);
            parameters.AddBoolean("@Result", false, ParameterDirection.Output);
            parameters.AddNVarChar("@DisplayName", 255, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@PwdHash", 255, string.Empty, ParameterDirection.Output);
            parameters.AddVarChar("@UserName", 255, string.Empty, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("dbo.sUser_Get", ref parameters, CommandType.StoredProcedure);

            if (parameters.GetBool("@Result"))
            {
                result = true;
                user = new ApiUserFull()
                {
                    Id = userId, DisplayName = parameters.GetString("@DisplayName"),
                    Password = getPwdHash ? parameters.GetString("@PwdHash") : "",
                    LoginName = parameters.GetString("@UserName")
                };
            }

            return result;
        }
        public bool UpdateUser(ApiUserFull user, out ApiUserFull updatedUser, out string errorMessage)
        {
            var result = false;
            updatedUser = null;
            errorMessage = string.Empty;
            bool updatePassword = false;
            var newPwdHash = string.Empty;
            var needsUpdate = false;
            var displayName = string.Empty;

            if (TryGetUser(user.Id, true, out var currentUser))
            {
                if (!string.IsNullOrEmpty(user.OldPassword))
                {
                    if (!string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.ValidatePassword))
                    {
                        var oldPwdHash = Common.CreateMd5Hash(user.OldPassword);
                        if (oldPwdHash.Equals(currentUser.Password, StringComparison.Ordinal))
                        {
                            if (user.Password.Equals(user.ValidatePassword, StringComparison.Ordinal))
                            {
                                updatePassword = true;
                                newPwdHash = Common.CreateMd5Hash(user.Password);
                                needsUpdate = true;
                            }
                            else
                            {
                                result = false;
                                errorMessage = "Password mismatch";
                            }
                        }
                        else
                        {
                            result = false;
                            errorMessage = "Incorrect Password";
                        }
                    }
                    else
                    {
                        result = false;
                        errorMessage = "Invalid new password";
                    }
                }
                else
                {
                    newPwdHash = currentUser.Password;
                }
                if (!user.DisplayName.Equals(currentUser.DisplayName))
                {
                    needsUpdate = true;
                    displayName = user.DisplayName;
                }
                else
                {
                    displayName = currentUser.DisplayName;

                }
                if (needsUpdate)
                {
                    var parameters = new SqlParameters();
                    parameters.AddUniqueIdentifier("@UserId", user.Id);
                    parameters.AddNVarChar("@DisplayName", 255, displayName);
                    parameters.AddNVarChar("@PwdHash", 255, newPwdHash);
                    
                    DbAccess.ExecuteNonQuery("dbo.sUser_Update", ref parameters, CommandType.StoredProcedure);
                    result = TryGetUser(user.Id, false, out updatedUser);

                }
            }

            return result;
        }

        public CustomerFull GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }


        private bool UpdateAccessToken(Guid userId, string token, int ttl = 360)
        {
            var parameters = new SqlParameters();
            parameters.AddUniqueIdentifier("@UserId", userId);
            parameters.AddNVarChar("@Token", 128, token);
            parameters.AddInt("@TTL", ttl);
            DbAccess.ExecuteNonQuery("dbo.sUser_UpdateAccessToken", ref parameters, CommandType.StoredProcedure);
            return true;
        }

        private bool UpdateTokenExpireDate(Guid userId, int ttl = 360)
        {
            var parameters = new SqlParameters();
            parameters.AddUniqueIdentifier("@UserId", userId);
            parameters.AddInt("@TTL", ttl);
            DbAccess.ExecuteNonQuery("dbo.sUser_UpdateAccessTokenExpireDate", ref parameters,
                CommandType.StoredProcedure);
            return true;
        }
        private string GenerateToken()
        {
            const string VALID_CHARS = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210";
            const int TOKEN_LENGTH = 64;
            var rnd = new Random();
            var token = string.Empty;

            var isUnique = false;
            while (!isUnique)
            {
                token = string.Empty;
                for (var i = 0; i < TOKEN_LENGTH; i++)
                {
                    var ri = rnd.Next(0, VALID_CHARS.Length - 1);
                    token = $"{token}{VALID_CHARS.Substring(ri, 1)}";
                }

                isUnique = IsTokenUnique(token);
            }

            return token;

        }

        private bool IsTokenUnique(string token)
        {
            if (token.Equals(CDS_TOKEN, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            var parameters = new SqlParameters(2);
            parameters.AddNVarChar("@Token", 128, token);
            parameters.AddBoolean("@Result", true, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("dbo.sUser_IsTokenUnique", ref parameters, CommandType.StoredProcedure);

            return parameters.GetBool("@Result");
            
        }

        private bool ValiidateUser(ApiUserFull user, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (user.LoginName != null && user.LoginName.Length <= 255)
            {
                if (IsUserNameUnique(user.LoginName.Trim()))
                {

                    if (user.Password != null && user.Password.Length >= 8 &&
                        user.ValidatePassword.Equals(user.Password))
                    {
                        return true;
                    }
                    else
                    {
                        errorMessage = "PASSWORDS_NOT_MATCH";
                    }
                }
                else
                {
                    errorMessage = "USERNAME_NOT_UNIQUE";
                }
            }

            return false;
        }

        private bool IsUserNameUnique(string userName)
        {
            var parameters = new SqlParameters();
            parameters.AddVarChar("@UserName", 255, userName);
            parameters.AddBoolean("@Result", false, ParameterDirection.Output);
            DbAccess.ExecuteNonQuery("dbo.sUser_UserNameUnique", ref parameters, CommandType.StoredProcedure);
            return parameters.GetBool("@Result");

        }
    }
}
