using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T,bool>> predicate=null,Func<IQueryable<T>,IQueryable<T>> include=null);
    }
}
