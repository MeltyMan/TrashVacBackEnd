using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVac.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public Enums.UserLevel UserLevel { get; set; }

    }
}
