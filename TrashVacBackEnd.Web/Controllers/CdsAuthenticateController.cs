using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashVac.Entity.Cds;
using TrashVacBackEnd.Core;
using TrashVacBackEnd.Core.Integrations;
using TrashVacBackEnd.Web.Attributes;

namespace TrashVacBackEnd.Web.Controllers
{
    [Route("api/cdsauthenticate")]
    [ApiController]
    public class CdsAuthenticateController : ControllerBase
    {
        public CdsAuthenticateController()
        {
            _cdsClient = new CdsClient();
        }
        private readonly CdsClient _cdsClient;

        [HttpGet]
        [Route("login")]
        public ActionResult<LoginResponse> LoginCds(string userName, string password)
        {
            var result = _cdsClient.Login(userName, password);
            if (result != null)
            {
                ServiceProvider.Current.InMemoryStorage.AddToken(result.AccessToken, result.Id);
                return new OkObjectResult(result);
            }

            return new UnauthorizedResult();

        }

        [CdsAuthorize]
        [HttpGet]
        [Route("dummy")]
        public ActionResult<string> Dummy(string text)
        {
            return new OkObjectResult($"Hello {text}");
        }

    }
}
