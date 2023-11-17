using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.ModelConfigurations
{
    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Mail).HasColumnName("Mail");
            builder.Property(e => e.PasswordSalt).HasColumnName("PasswordSalt");
            builder.Property(e => e.PasswordHash).HasColumnName("PasswordHash");
        }
    }

    public class ClaimModelConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable("Claims");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.HasData(
                new Claim { Id = 1, Name = "Product.Get" },
                new Claim { Id = 2, Name = "Product.Add" },
                new Claim { Id = 3, Name = "Product.Update" },
                new Claim { Id = 4, Name = "Product.Delete" },
                new Claim { Id = 5, Name = "Product.Create" }
            );
        }
    }

    public class UserClaimModelConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).HasColumnName("UserId");
            builder.HasOne(e => e.User).WithMany(u => u.UserClaims).HasForeignKey(e => e.UserId);
            builder.Property(e => e.ClaimId).HasColumnName("ClaimId");
            builder.HasOne(e => e.Claim).WithMany(c => c.UserClaims).HasForeignKey(e => e.ClaimId);
        }
    }
}
