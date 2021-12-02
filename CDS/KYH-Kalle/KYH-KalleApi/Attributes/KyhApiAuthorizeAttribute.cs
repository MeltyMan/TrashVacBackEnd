using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KYH_KalleApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KYH_KalleApi.Attributes
{
    public class KyhApiAuthorizeAttribute : TypeFilterAttribute
    {
        public KyhApiAuthorizeAttribute() : this(true)
        {

        }

        public KyhApiAuthorizeAttribute(bool requireToken) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim("requiretoken", requireToken.ToString()) };
        }
    }
}
