using System;
using System.Collections.Generic;
using System.Text;

namespace KYH_Kalle.Core.Entity
{
    public class ApiUserFull : ApiUserMini
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ValidatePassword { get; set; }
        public string LoginName { get; set; }
        
    }
}
