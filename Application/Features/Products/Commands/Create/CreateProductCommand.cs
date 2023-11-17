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

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand:IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public double Price{ get; set; }
    }

    public class CreateProductCommandHadler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHadler(IProductWriteRepository productWriteRepository, IMapper mapper)
        {
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product productToAdd = new()
            {
                Name = request.Name,
                Price = request.Price,
            };

            Task<Product> addedProduct= _productWriteRepository.AddAsync(productToAdd);

            CreateProductCommandResponse response = new();
            response.CreatedProduct= _mapper.Map<ProductDto>(await addedProduct);

            return response;
        }
    }

}
