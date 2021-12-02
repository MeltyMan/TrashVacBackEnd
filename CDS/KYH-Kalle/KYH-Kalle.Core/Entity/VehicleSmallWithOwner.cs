using System;
using System.Collections.Generic;
using System.Text;

namespace KYH_Kalle.Core.Entity
{
    public class VehicleSmallWithOwner : VehicleSmall
    {
        public Enums.OwnerStatus OwnerStatus { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
