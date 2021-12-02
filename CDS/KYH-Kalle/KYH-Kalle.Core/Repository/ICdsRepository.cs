using System;
using System.Collections.Generic;
using KYH_Kalle.Core.Entity;

namespace KYH_Kalle.Core.Repository
{
    public interface ICdsRepository
    {
        int CreateVehicleList(int count);
        IList<CustomerFull> GetCustomerList(string filter, bool wildCard);
        bool TryGetVehicleByVin(string vin, out Vehicle vehicle);
        bool TryGetVehicleByVinAndRegNo(string vin, string regNo, out Vehicle vehicle);
        bool TryGetVehicleByVinRegNoAndCustomerId(string vin, string regNo, Guid customerId, out Vehicle vehicle);
        IList<Vehicle> GetVehicleList(string regNo);
        bool UpdateOwner(string vin, Guid customerId, Enums.OwnerStatus status, out Vehicle vehicle);
        CustomerFull GetCustomer(Guid customerId);

    }
}