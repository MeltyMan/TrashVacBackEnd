using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVac.Entity
{
    public class UserWithTags : User
    {
        public UserWithTags()
        {
            Tags = new List<RfIdTag>();
        }
        public IList<RfIdTag> Tags { get; set; }
    }
}
