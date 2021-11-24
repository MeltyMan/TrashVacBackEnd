using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrashVac.Entity;
using TrashVacBackEnd.Core;
using TrashVacBackEnd.Core.Repository;
using TrashVacBackEnd.Web.Attributes;

namespace TrashVacBackEnd.Web.Controllers
{
    [Route("api/rfid")]
    [ApiController]
    public class RfIdController : ControllerBase
    {
        private readonly IRfIdRepository _rfIdRepository;
        private readonly ILogRepository _logRepository;

        public RfIdController()
        {
            _rfIdRepository = Injector.GetInstance<IRfIdRepository>();
            _logRepository = Injector.GetInstance<ILogRepository>();
        }

        [TrashVacStaticAuthorize]
        [HttpGet]
        [Route("{rfId}/{doorId}/validate")]
        public ActionResult<ValidateRfIdResponse> ValidateRfId(string rfId, string doorId)
        {
            var response = _rfIdRepository.ValidateRfIdAccess(rfId, doorId);
            _logRepository.WriteToDoorAccessLog(rfId, doorId, response.User != null ? response.User.Id : Guid.Empty,
                response.IsValid);
            if (response.IsValid)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new UnauthorizedObjectResult(response);
            }
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<IList<RfIdTag>> GetRfIdTagList()
        {
            return new OkObjectResult(_rfIdRepository.GetList());
        }

    }
}
