using Application.Abstractions.Persistence.Repositories.Claims;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.Claims
{
    public class ClaimWriteRepository : WriteRepository<Claim>, IClaimWriteRepository
    {
        public ClaimWriteRepository(ProductManagementContext context) : base(context)
        {
        }
    }
}
