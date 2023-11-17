using Application.Abstractions.Persistence.Repositories.Claims;
using Application.Abstractions.Persistence.Repositories.Products;
using Application.Abstractions.Persistence.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Claims;
using Persistence.Repositories.Products;
using Persistence.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceDependencyResolver
    {
        public static void AddPersistenceServices(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<ProductManagementContext>(options =>
            {
                options.UseSqlServer(configuration["DbConnectionString"]);
            });

            //Repositories
            services.AddScoped<IUserReadRepository,UserReadRepository>();
            services.AddScoped<IUserWriteRepository,UserWriteRepository>();

            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();

            services.AddScoped<IClaimReadRepository, ClaimReadRepository>();
            services.AddScoped<IClaimWriteRepository, ClaimWriteRepository>();
            //Services



        }
    }
}
