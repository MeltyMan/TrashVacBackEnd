using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TrashVac.Entity;
using TrashVac.Entity.Dto;
using TrashVacBackEnd.Core;
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
            _userRepository = Injector.GetInstance<IUserRepository>();

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
        
        public ActionResult<Guid> CreateUser(UserFull user)
        {
            var userId = _userRepository.CreateUser(user);
            if (!userId.Equals(Guid.Empty))
            {
                return new OkObjectResult(userId);
            }
            else
            {
                return new BadRequestResult();
            }


        }

        [HttpGet]
        [Route("search")]
        public ActionResult<IList<User>> SearchUser(string searchString)
        {
            return new OkObjectResult(_userRepository.SearchUser(searchString));
        }

        [HttpGet]
        [Route("{userId}/rfidtags")]
        public ActionResult<UserWithTags> GetUserTags(Guid userId)
        {
            var user = _userRepository.GetUserTags(userId);
            if (user != null)
            {
                return new OkObjectResult(user);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Route("{userId}/tag")]
        public ActionResult<bool> PersistUserTagRelation(Guid userId, UserRfIdRelation dto)
        {
            var result = _userRepository.PersistUserTagRelation(userId, dto);

            switch (result)
            {
                case Enums.PersistUserTagRelationStatus.Success:
                    return new OkObjectResult(true);
                case Enums.PersistUserTagRelationStatus.InvalidDto:
                    return new BadRequestResult();
                default:
                    return new NotFoundResult();
            }
            
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
