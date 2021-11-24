using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashVac.Entity;
using TrashVacBackEnd.Core;
using TrashVacBackEnd.Core.Handlers;

namespace TrashVacBackEnd.Web.Controllers
{
    [Route("api/kyhdemo")]
    [ApiController]
    public class KyhDemoController : ControllerBase
    {

        private readonly IMessageHandler _messageHandler;

        public KyhDemoController()
        {
            _messageHandler = Injector.GetInstance<IMessageHandler>();
        }

        [HttpGet]
        [Route("message")]
        public ActionResult<string> SendMessage(Enums.MessageType messageType, string message)
        {
            var result = _messageHandler.TryGetResponse(messageType, message, out var response);

            if (result)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new BadRequestResult();
            }

        }


    }
}
