using System;
using System.Collections.Generic;
using System.Text;

namespace KYH_Kalle.Core.Entity
{
    public class Vehicle : VehicleSmall
    {
       
        public CustomerOwner Owner { get; set; }
    }
}
