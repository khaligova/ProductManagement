using Application.Abstractions.Persistence.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ProductManagementContext _context;
        public ReadRepository(ProductManagementContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            var query=Table.AsQueryable();

            if (predicate is not null)
               query=query.Where(predicate);

            if (include is not null)
                query=include(query);

            return query;
        }

        public IQueryable<T> GetAll()
            => Table;

        public Task<T> GetById(int id)
        => Table.Where(x => x.Id == id).FirstOrDefaultAsync();

        
    }
}
