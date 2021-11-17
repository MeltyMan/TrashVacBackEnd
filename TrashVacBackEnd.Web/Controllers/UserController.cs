using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashVacBackEnd.Core;
using TrashVacBackEnd.Core.Entity;
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
        public IList<User> ListUsers()
        {
            return _userRepository.GetUserList();
        }

        [HttpPost]
        [Route("")]
        [TrashVacAuthorize(Enums.UserLevel.Admin)]
        public Guid CreateUser(UserFull user)
        {
            return _userRepository.CreateUser(user);
        }

        [HttpGet]
        [Route("login")]
        public UserAuthenticated Login(string userName, string password)
        {
            if (_userRepository.TryLogin(userName, password, out var user))
            {
                return user;
            }
            else
            {
                return null;
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
