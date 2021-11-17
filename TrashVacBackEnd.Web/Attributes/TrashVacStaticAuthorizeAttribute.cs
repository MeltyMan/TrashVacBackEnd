using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrashVacBackEnd.Web.Filters;

namespace TrashVacBackEnd.Web.Attributes
{
    public class TrashVacStaticAuthorizeAttribute : TypeFilterAttribute
    {
        public TrashVacStaticAuthorizeAttribute() : this(false){}
        public TrashVacStaticAuthorizeAttribute(bool requireFilter) : base(typeof(ClaimStaticFilter))
        {
            Arguments = new object[] { new Claim("requiretoken", requireFilter.ToString()) };
        }
    }
}
