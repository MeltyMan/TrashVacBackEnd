using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVac.Entity.Cds
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
    }
}
