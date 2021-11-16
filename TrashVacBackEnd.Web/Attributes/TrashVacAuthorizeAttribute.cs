using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrashVacBackEnd.Core;
using TrashVacBackEnd.Web.Filters;

namespace TrashVacBackEnd.Web.Attributes
{
    public class TrashVacAuthorizeAttribute : TypeFilterAttribute
    {
        public TrashVacAuthorizeAttribute() : this(true, Enums.UserLevel.Undefined){}
        public TrashVacAuthorizeAttribute(Enums.UserLevel minimumUserLevel) : this(true, minimumUserLevel){}
        public TrashVacAuthorizeAttribute(bool requireToken, Enums.UserLevel minimumUserLevel) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim("requiretoken", requireToken.ToString()) };
        }
    }
}
