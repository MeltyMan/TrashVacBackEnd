using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashVacTestClient.Win.Entity
{
    public class AccessResult
    {
        public string RfId { get; set; }
        public bool IsValid { get; set; }
        public User User { get; set; }
    }
}
