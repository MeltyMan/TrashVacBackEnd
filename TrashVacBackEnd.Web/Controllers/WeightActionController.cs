using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashVacBackEnd.Core;
using TrashVacBackEnd.Core.Repository;
using TrashVacBackEnd.Web.Attributes;

namespace TrashVacBackEnd.Web.Controllers
{
    [Route("api/weightaction")]
    [ApiController]
    public class WeightActionController : ControllerBase
    {

        private readonly IWeightActionRepository _weightActionRepository;

        public WeightActionController()
        {
            _weightActionRepository = Injector.GetInstance<IWeightActionRepository>();
        }

        [HttpPost]
        [Route("initialize")]
        [TrashVacStaticAuthorize]
        public ActionResult<Guid> InitializeWeightAction(Guid userId, string doorId)
        {

            return new OkObjectResult(_weightActionRepository.Initialize(userId, doorId));

        }

    }
}
