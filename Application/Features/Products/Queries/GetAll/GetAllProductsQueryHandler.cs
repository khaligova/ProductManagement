using Application.Abstractions.Persistence.Repositories.Products;
using Application.Features.Products.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public Task<GetAllProductsQueryResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> allProducts = _productReadRepository.GetAll().ToList();
            
            GetAllProductsQueryResponse response = new();
            response.ProductListDto = _mapper.Map<List<ProductDto>>(allProducts);

            return Task.FromResult(response);
        }
    }
}
