using Application.Abstractions.Persistence.Repositories.Products;
using Application.Features.Products.Dtos;
using Application.Features.Products.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        private readonly ProductRuleManager _productRuleManager;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(ProductRuleManager productRuleManager, IMapper mapper)
        {
            _productRuleManager = productRuleManager;
            _mapper = mapper;
        }

        public Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product;
            _productRuleManager
                .CheckIfProductExist(request.Id,out product);

            GetProductByIdQueryResponse response = new();
            response.ProductDto = _mapper.Map<ProductDto>(product);

            return Task.FromResult(response);
        }
    }
}
