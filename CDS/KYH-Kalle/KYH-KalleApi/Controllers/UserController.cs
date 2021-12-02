using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KYH_Kalle.Core;
using KYH_Kalle.Core.Entity;
using KYH_Kalle.Core.Repository;
using KYH_KalleApi.Attributes;

namespace KYH_KalleApi.Controllers
{
   
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = Injector.GetInstance<IUserRepository>();
        }

     
        [HttpGet]
        [Route("ping")]
        public string Ping()
        {
            return "pong";
        }

        [KyhApiAuthorize]
        [HttpGet]
        [Route("list")]
        public ActionResult<IList<ApiUserMini>> GetUserList()
        {
            return new ActionResult<IList<ApiUserMini>>(_userRepository.GetUserList());
        }
        [KyhApiAuthorize]
        [HttpPost]
        [Route("")]
        public ActionResult<string> CreateUser(ApiUserFull user)
        {
            var result = _userRepository.CreateUser(user, out var errorMessage);
            if (result == Guid.Empty)
            {
                return new BadRequestObjectResult(errorMessage);
            }
            else
            {
                return new OkObjectResult(result.ToString());
            }
        }

        [KyhApiAuthorize]
        [HttpPost]
        [Route("customer/connect/{userId}/{customerId}")]
        public ActionResult<string> ConnectCustomer(string userId, string customerId)
        {
            var result = _userRepository.ConnectCustomer(new Guid(userId), new Guid(customerId));
            return new OkObjectResult(result.ToString());
        }

        [HttpGet]
        [Route("login")]
        public ActionResult<ApiUserMini> Login(string userName, string pwd)
        {
            if (_userRepository.TryLogin(userName, pwd, out var user))
            {
                return new OkObjectResult(user);
            }
            else
            {
                return new UnauthorizedObjectResult("INVALID_LOGIN");
            }
        }

    }
}
