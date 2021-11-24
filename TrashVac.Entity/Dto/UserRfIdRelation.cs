using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVac.Entity.Dto
{
    public class UserRfIdRelation
    {
        public Guid UserId { get; set; }
        public string RfId { get; set; }
        public bool Deleted { get; set; }
    }
}
