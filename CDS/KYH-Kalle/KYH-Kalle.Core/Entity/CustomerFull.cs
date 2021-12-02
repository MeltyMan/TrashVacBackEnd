using System;
using System.Collections.Generic;
using System.Text;

namespace KYH_Kalle.Core.Entity
{
    public class CustomerFull : Customer
    {
        public CustomerFull()
        {
            this.Vehicles = new List<VehicleSmallWithOwner>();
        }
       
        public IList<VehicleSmallWithOwner> Vehicles { get; set; }



    }
}
