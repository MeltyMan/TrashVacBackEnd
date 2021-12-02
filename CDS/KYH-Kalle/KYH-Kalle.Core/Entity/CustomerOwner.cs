using System;
using System.Collections.Generic;
using System.Text;

namespace KYH_Kalle.Core.Entity
{
    public class CustomerOwner : Customer
    {
        public Enums.OwnerStatus OwnerStatus { get; set; }
    }
}
