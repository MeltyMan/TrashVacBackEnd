using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TrashVac.Entity;
using TrashVacBackEnd.Core.Repository;
using TrashVacBackEnd.Web.Attributes;

namespace TrashVacBackEnd.Web.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            _userRepository = new SqlUserRepository();

        }

        private readonly IUserRepository _userRepository;

       
        [HttpGet]
        [Route("list")]
        [TrashVacAuthorize(Enums.UserLevel.Admin)]
        public ActionResult<IList<User>> ListUsers()
        {
            return new OkObjectResult(_userRepository.GetUserList());
        }

        [HttpPost]
        [Route("")]
        [TrashVacAuthorize(Enums.UserLevel.Admin)]
        public ActionResult<Guid> CreateUser(UserFull user)
        {
            return new OkObjectResult(_userRepository.CreateUser(user));
        }

        [HttpGet]
        [Route("login")]
        public ActionResult<UserAuthenticated> Login(string userName, string password)
        {
            if (_userRepository.TryLogin(userName, password, out var user))
            {
                return new OkObjectResult(user);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }

#if DEBUG
        [HttpGet]
        [Route("test/validatetoken/{token}")]
        public bool TestValidateToken(string token)
        {
            return _userRepository.ValidateToken(token, out var user);
        }
#endif
    }
}
