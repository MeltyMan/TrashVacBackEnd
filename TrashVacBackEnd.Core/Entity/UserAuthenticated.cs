using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVacBackEnd.Core.Entity
{
    public class UserAuthenticated : User
    {
        public string AccessToken { get; set; }
    }
}
