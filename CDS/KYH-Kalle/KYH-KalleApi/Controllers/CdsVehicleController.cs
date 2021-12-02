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
    [Route("api/cds/v1.0/vehicle")]
    [ApiController]
    public class CdsVehicleController : ControllerBase
    {

        private readonly ICdsRepository _cdsRepository;

        public CdsVehicleController()
        {
            _cdsRepository = Injector.GetInstance<ICdsRepository>();
        }

        

        [HttpGet]
        [Route("list")]
        public ActionResult<IList<Vehicle>> GetVehicleList(string regNo = "")
        {
            var result = _cdsRepository.GetVehicleList(regNo);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [Route("owner/{vin}/{customerId}")]
        public ActionResult<Vehicle> UpdateVehicleOwner(string vin, string customerId)
        {
            if (_cdsRepository.UpdateOwner(vin, new Guid(customerId), Enums.OwnerStatus.Owning, out var vehicle))
            {
                return new OkObjectResult(vehicle);
            }

            return new BadRequestResult();
        }

        [HttpGet]
        [Route("{vin}/{regNo}")]
        public ActionResult<Vehicle> TryGetVehicleByVinAndRegNo(string vin, string regNo)
        {
            if(_cdsRepository.TryGetVehicleByVinAndRegNo(vin, regNo, out var vehicle))
            {
                return new OkObjectResult(vehicle);
            }

            return new NotFoundResult();
        }

        [HttpGet]
        [Route("{vin}/{regNo}/{customerId}")]
        public ActionResult<Vehicle> TryGetVehicleByVinAndRegNo(string vin, string regNo, string customerId)
        {
            if (_cdsRepository.TryGetVehicleByVinRegNoAndCustomerId(vin, regNo, new Guid(customerId), out var vehicle))
            {
                return new OkObjectResult(vehicle);
            }

            return new NotFoundResult();
        }



#if DEBUG
        [HttpGet]
        [Route("createlist")]
        public ActionResult<int> CreateVehicleList()
        {
            return _cdsRepository.CreateVehicleList(100);

        }
#endif

    }
}
