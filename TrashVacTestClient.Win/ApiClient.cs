using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TrashVacTestClient.Win.Entity;

namespace TrashVacTestClient.Win
{
    public class ApiClient
    {
        private const string STATIC_TOKEN = "IoT21KalleKalle";
        public ApiClient()
        {
            _client = new RestClient("https://localhost:44366");
        }

        private readonly IRestClient _client;

        public AccessResult ValidateAccess(string rfId, string doorId)
        {
            var request = new RestRequest($"/api/rfid/{rfId}/{doorId}/validate", Method.GET);
            request.AddHeader("trashvac-auth", STATIC_TOKEN);

            var response = _client.Execute(request);

            if (response.IsSuccessful)
            {
                var json = response.Content;

                var result = JsonConvert.DeserializeObject<AccessResult>(json);
                return result;

            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var json = response.Content;
                    var result = JsonConvert.DeserializeObject<AccessResult>(json);
                    return result;
                }
            }

            return null;
        }


    }
}
