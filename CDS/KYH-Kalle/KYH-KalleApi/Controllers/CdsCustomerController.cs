using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KYH_Kalle.Core;
using KYH_Kalle.Core.Entity;
using KYH_Kalle.Core.Repository;
using KYH_KalleApi.Attributes;

namespace KYH_KalleApi.Controllers
{
    [KyhApiAuthorize]
    [Route("cds/v1.0/customer")]
    [ApiController]
    public class CdsCustomerController : ControllerBase
    {
        private readonly ICdsRepository _cdsRepository;

        public CdsCustomerController()
        {
            _cdsRepository = Injector.GetInstance<ICdsRepository>();
        }

        
        [HttpGet]
        [Route("list/all")]
        public ActionResult<IList<CustomerFull>> GetCustomerList(string filter, bool wildCard = false)
        {
            var result = _cdsRepository.GetCustomerList(filter == null ? "" : filter, wildCard);
            return new OkObjectResult(result);
        }

        
        [HttpGet]
        [Route("{customerId}")]
        public ActionResult<CustomerFull> GetCustomer(Guid customerId)
        {
            var customer = _cdsRepository.GetCustomer(customerId);
            if (customer == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(customer);
        }
    }
}
