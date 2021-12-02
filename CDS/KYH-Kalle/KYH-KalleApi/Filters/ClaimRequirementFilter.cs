using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KYH_Kalle.Core;
using KYH_Kalle.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KYH_KalleApi.Filters
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly IUserRepository _userRepository;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
            _userRepository = Injector.GetInstance<IUserRepository>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = false;
            if (_claim.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                var authenticationHeader = context.HttpContext.Request.Headers["kyh-auth"];
                if (authenticationHeader.Count == 1)
                {
                    var token = authenticationHeader[0];
                    hasClaim = _userRepository.ValidateToken(token, out var userId);

                }
            }
            else
            {
                hasClaim = true;
            }

            if (!hasClaim)
            {
                context.Result = new UnauthorizedResult();
            }
        }

    }
}
