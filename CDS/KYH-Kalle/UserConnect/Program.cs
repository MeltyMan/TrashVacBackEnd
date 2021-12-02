using System;
using System.Data;
using HiQ.NetStandard.Util.Data;
using KYH_Kalle.Core.Entity;
using RestSharp;

namespace UserConnect
{
    class Program
    {
        static void Main(string[] args)
        {

            var restClient = new RestClient("https://localhost:44390");
            var dbAccess =
                new SqlDbAccess(
                    "Server=tcp:10.56.18.50,1433;Initial Catalog=KYH-Kalle;Integrated Security=SSPI;Persist Security Info=True;Connection Timeout=30;");

            var sql = "SELECT c.CustomerId, c.FirstName, c.LastName FROM dbo.CdsCustomers AS c WHERE c.UserId IS NULL;";
            var dr = dbAccess.ExecuteReader(sql, CommandType.Text);



            while (dr.Read())
            {

                var loginName = $"{dr.GetString(1).Substring(0, 2)}{dr.GetString(2).Substring(0, 4)}";


                var user = new ApiUserFull()
                {
                    Id = Guid.Empty, DisplayName = $"{dr.GetString(1)} {dr.GetString(2)}", Password = "IoT20!!!",
                    ValidatePassword = "IoT20!!!", LoginName = loginName
                };

                var request = new RestRequest("api/user", Method.POST);
                request.AddJsonBody(user);
                request.AddHeader("kyh-auth", "meltyman");
                var response = restClient.Execute(request);
                if (response.IsSuccessful)
                {
                    var userId = response.Content.Replace("\"", "");

                    request = new RestRequest($"api/user/customer/connect/{userId}/{dr.GetGuid(0)}", Method.POST);
                    request.AddHeader("kyh-auth", "meltyman");
                    response = restClient.Execute(request);

                }

                


            }

            dbAccess.DisposeReader(ref dr);

        }
    }
}
