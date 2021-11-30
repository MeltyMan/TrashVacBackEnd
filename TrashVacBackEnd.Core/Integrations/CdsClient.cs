using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using TrashVac.Entity.Cds;

namespace TrashVacBackEnd.Core.Integrations
{
    public class CdsClient
    {
        private readonly IRestClient _restClient;

        public CdsClient()
        {
            _restClient = new RestClient("https://kyhdev.hiqcloud.net");
        }


        public LoginResponse Login(string userName, string password)
        {
            var request =
                new RestRequest(
                    $"api/api/cds/v1.0/user/authenticate?userName={HttpUtility.UrlEncode(userName)}&pwd={HttpUtility.UrlEncode(password)}",
                    Method.GET);
            return Execute<LoginResponse>(request);
        }

        public bool ValidateToken(Guid userId, string token)
        {
            var request =
                new RestRequest($"api/api/cds/v1.0/user/{userId}/validate?token={HttpUtility.UrlEncode(token)}");

            return Execute<bool>(request);
        }
        private T Execute<T>(IRestRequest request)
        {
            var response = _restClient.Execute(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<T>(response.Content);
                return (T)(object)result;
            }

            return default(T);

        }

    }
}
