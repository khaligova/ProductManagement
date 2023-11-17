using Application.Abstractions.Persistence.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ProductManagementContext _context;

        public WriteRepository(ProductManagementContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async void AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public T Delete(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public async Task<T> DeleteByIdAsync(int id)
        {
            T model = await Table.FirstOrDefaultAsync(x => x.Id == id);
            return Delete(model);
        }


        public T Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

    }
}
