using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashVac.Entity;
using TrashVacBackEnd.Core;
using TrashVacBackEnd.Core.Repository;

namespace TrashVacBackEnd.Web.Controllers
{
    [Route("api/door")]
    [ApiController]
    public class DoorController : ControllerBase
    {
        private readonly IDoorRepository _doorRepository;

        public DoorController()
        {
            _doorRepository = Injector.GetInstance<IDoorRepository>();
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<IList<DoorWithAccess>> GetDoorListWithAccess(string rfId)
        {
            return new OkObjectResult(_doorRepository.GetDoorListWithAccess(rfId));
        }

        [HttpPost]
        [Route("access/{rfId}")]
        public ActionResult<bool> PersistDoorAccess(string rfId, IList<DoorWithAccess> dto)
        {
            return new OkObjectResult(_doorRepository.PersistDoorAccess(rfId, dto));
        }
    }
}
