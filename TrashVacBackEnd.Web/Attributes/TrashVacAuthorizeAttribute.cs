using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TrashVac.Entity;
using TrashVacBackEnd.Web.Filters;

namespace TrashVacBackEnd.Web.Attributes
{
    public class TrashVacAuthorizeAttribute : TypeFilterAttribute
    {
        public TrashVacAuthorizeAttribute() : this(true, Enums.UserLevel.Undefined){}
        public TrashVacAuthorizeAttribute(Enums.UserLevel minimumUserLevel) : this(true, minimumUserLevel){}
        public TrashVacAuthorizeAttribute(bool requireToken, Enums.UserLevel minimumUserLevel) : base(typeof(ClaimRequirementFilter))
        {
            var claimTypeCollection = new ClaimTypeCollection();
            claimTypeCollection.ClaimTypes.Add(new ClaimTypeValuePair(){Key = "requiretoken", Value = requireToken.ToString()});
            claimTypeCollection.ClaimTypes.Add(new ClaimTypeValuePair()
                { Key = "minuserlevel", Value = minimumUserLevel.ToString() });

            Arguments = new object[]
                { new Claim("claimtypecollection", JsonConvert.SerializeObject(claimTypeCollection)) };
        }
    }
}
