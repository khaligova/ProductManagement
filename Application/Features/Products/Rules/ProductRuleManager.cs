using Application.Abstractions.Persistence.Repositories.Products;
using Application.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Rules
{
    public class ProductRuleManager
    {
        private readonly IProductReadRepository _productReadRepository;

        public ProductRuleManager(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public ProductRuleManager CheckIfProductExist(int id,out Product product)
        {
             product =_productReadRepository.GetById(id).Result;
            if (product == null)
                throw new BusinessException("This product doesn't exist.");

            return this;
        }
    }
}
