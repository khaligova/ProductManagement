using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Products;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Products
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ProductManagementContext context) : base(context)
        {
        }
    }
}
