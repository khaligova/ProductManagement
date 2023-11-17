using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Claim:BaseEntity
    {
        public string Name { get; set; }
        public List<UserClaim> UserClaims { get; set; }
    }
}
