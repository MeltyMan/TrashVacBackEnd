using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrashVacBackEnd.Core.Repository;

namespace TrashVacBackEnd.Web.Filters
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
            _userRepository = new SqlUserRepository();
        }

        private readonly Claim _claim;
        private readonly IUserRepository _userRepository;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = false;

            if (_claim.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                var authHeader = context.HttpContext.Request.Headers["trashvac-auth"];
                if (authHeader.Count == 1)
                {
                    var token = authHeader[0];
                    hasClaim = _userRepository.ValidateToken(token, out var user);
                }
            }

            if (!hasClaim)
            {
                context.Result = new UnauthorizedResult();
            }

        }
    }
}
