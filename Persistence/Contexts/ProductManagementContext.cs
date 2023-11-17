using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.ModelConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class ProductManagementContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Claim> Claims { get; set; }


        public ProductManagementContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductModelConfiguration());
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());
            modelBuilder.ApplyConfiguration(new ClaimModelConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimModelConfiguration());
        }


    }
}
