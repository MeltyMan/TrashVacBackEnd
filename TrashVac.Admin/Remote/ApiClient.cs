using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using TrashVac.Entity;
using TrashVac.Entity.Dto;

namespace TrashVac.Admin.Remote
{
    public class ApiClient
    {
        private readonly IRestClient _restClient;
        private string _accessToken;

        public ApiClient()
        {
#if DEBUG
            _restClient = new RestClient("https://localhost:44366");
#else
            _restClient = new RestClient("https://trashvacstage.hiqcloud.net");
#endif
        }

        public string AccessToken
        {
            get { return _accessToken;}
            set { _accessToken = value; }
        }

        public UserAuthenticated Login(string userName, string password)
        {
            var request =
                new RestRequest(
                    $"/api/user/login?userName={HttpUtility.UrlEncode(userName)}&password={HttpUtility.UrlEncode(password)}",
                    Method.GET);

            return Execute<UserAuthenticated>(request, false);
           
        }

        public IList<User> GetUserList()
        {
            var request = new RestRequest("api/user/list", Method.GET);

            return Execute<IList<User>>(request);

        }

        public IList<User> SearchUser(string searchString)
        {
            var request = new RestRequest($"api/user/search?searchString={HttpUtility.UrlEncode(searchString)}",
                Method.GET);
            return Execute<IList<User>>(request);
        }

        public UserWithTags GetUserWithTags(Guid userId)
        {
            var request = new RestRequest($"api/user/{userId}/rfidtags", Method.GET);

            return Execute<UserWithTags>(request);

        }
        public Guid AddUser(UserFull user)
        {
            var request = new RestRequest("api/user", Method.POST);
            request.AddJsonBody(user);
            return Execute<Guid>(request);

        }

        public IList<RfIdTag> GetTagList()
        {
            var request = new RestRequest("/api/rfid/list", Method.GET);
            return Execute<IList<RfIdTag>>(request);
        }

        public bool PersistUserTagRelation(Guid userId, UserRfIdRelation dto)
        {
            var request = new RestRequest($"/api/user/{userId}/tag", Method.POST);
            request.AddJsonBody(dto);
            return Execute<bool>(request);
        }

        public IList<DoorWithAccess> GetDoorListWithAccess(string rfId)
        {
            var request = new RestRequest($"api/door/list?rfId={HttpUtility.UrlEncode(rfId)}", Method.GET);
            return Execute<IList<DoorWithAccess>>(request);
        }

        public bool PersistDoorAccess(string rfId, IList<DoorWithAccess> dto)
        {
            var request = new RestRequest($"api/door/access/{rfId}", Method.POST);
            request.AddJsonBody(dto);
            return Execute<bool>(request);
        }
        private T Execute<T>(IRestRequest request, bool useToken = true)
        {
            if (useToken && !string.IsNullOrEmpty(_accessToken))
            {
                request.AddHeader("trashvac-auth", _accessToken);
            }

            IRestResponse response;
            lock (_restClient)
            {
                response = _restClient.Execute(request);
            }

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }

            return default(T);
        }

    }
}
