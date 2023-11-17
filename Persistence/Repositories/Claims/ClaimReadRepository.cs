using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Claims;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Claims
{
    public class ClaimReadRepository : ReadRepository<Claim>, IClaimReadRepository
    {
        public ClaimReadRepository(ProductManagementContext context) : base(context)
        {
        }
    }
}
