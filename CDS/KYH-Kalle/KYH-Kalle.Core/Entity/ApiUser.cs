using System;
using System.Collections.Generic;
using System.Text;

namespace KYH_Kalle.Core.Entity
{
    public class ApiUser : ApiUserMini
    {
        public string AccessToken { get; set; }
        public Guid CustomerId { get; set; }

    }
}
