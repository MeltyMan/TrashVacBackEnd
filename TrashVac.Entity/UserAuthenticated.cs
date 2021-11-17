using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVac.Entity
{
    public class UserAuthenticated : User
    {
        public string AccessToken { get; set; }
    }
}
