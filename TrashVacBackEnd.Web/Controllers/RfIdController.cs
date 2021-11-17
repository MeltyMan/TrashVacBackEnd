using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashVacBackEnd.Core.Entity;
using TrashVacBackEnd.Core.Extensions;
using TrashVacBackEnd.Core.Repository;
using TrashVacBackEnd.Web.Attributes;

namespace TrashVacBackEnd.Web.Controllers
{
    [Route("api/rfid")]
    [ApiController]
    public class RfIdController : ControllerBase
    {
        private readonly IRfIdRepository _rfIdRepository;

        public RfIdController()
        {
            _rfIdRepository = new SqlRfIdRepository();
        }

        [TrashVacStaticAuthorize]
        [HttpGet]
        [Route("{rfId}/{doorId}/validate")]
        public ActionResult<ValidateRfIdResponse> ValidateRfId(string rfId, string doorId)
        {
            var response = _rfIdRepository.ValidateRfIdAccess(rfId, doorId);

            if (response.IsValid)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new UnauthorizedObjectResult(response);
            }
        }

    }
}
