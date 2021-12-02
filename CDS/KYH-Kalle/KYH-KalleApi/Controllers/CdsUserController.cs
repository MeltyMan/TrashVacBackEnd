using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using KYH_Kalle.Core;
using KYH_Kalle.Core.Entity;
using KYH_Kalle.Core.Repository;
using KYH_KalleApi.Attributes;

namespace KYH_KalleApi.Controllers
{
    [Route("api/cds/v1.0/user")]
    [ApiController]
    public class CdsUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public CdsUserController()
        {
            _userRepository = Injector.GetInstance<IUserRepository>();
        }

        [HttpGet]
        [Route("authenticate")]
        public ActionResult<ApiUser> Login(string userName, string pwd)
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

        [HttpGet]
        [Route("{userId}/validate")]
        public ActionResult<bool> ValidateToken(Guid userId, string token)
        {
            var result = _userRepository.ValidateToken(token, out var tokenUserId);
            if (result)
            {
                if (tokenUserId.Equals(userId))
                {
                    return new OkObjectResult(true);
                }
                else
                {
                    return new BadRequestObjectResult(false);
                }
            }
            else
            {
                return new UnauthorizedObjectResult(false);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        [Description("Gets the user")]
        public ActionResult<CustomerFull> GetUser(Guid userId)
        {
            return new OkObjectResult(new CustomerFull());

        }
        [KyhApiAuthorize]
        [HttpPatch]
        [Route("")]
        public ActionResult<ApiUser> UpdateUser(ApiUserFull user)
        {
            if (_userRepository.UpdateUser(user, out var updatedUser, out var errorMessage))
            {
                return new OkObjectResult(updatedUser);
            }

            return new BadRequestObjectResult(errorMessage);
        }

        [KyhApiAuthorize]
        [HttpPost]
        [Route("")]
        public ActionResult<ApiUser> AddUser(ApiUserFull user)
        {
            return new OkObjectResult(user);

            //return new BadRequestObjectResult(errorMessage);
        }
    }
}
