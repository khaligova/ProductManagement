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

namespace Application.Features.Products.Commands.DeleteById
{
    public class DeleteProductByIdCommand : IRequest<DeleteProductByIdCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, DeleteProductByIdCommandResponse>
    {
        private readonly ProductRuleManager _productRuleManager;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;
        public DeleteProductByIdCommandHandler(ProductRuleManager productRuleManager, IProductWriteRepository productWriteRepository, IMapper mapper)
        {
            _productRuleManager = productRuleManager;
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
        }

        public Task<DeleteProductByIdCommandResponse> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            Product targetProduct;

            _productRuleManager
                .CheckIfProductExist(request.Id, out targetProduct);

            Product deletedProduct = _productWriteRepository.Delete(targetProduct);

            DeleteProductByIdCommandResponse response = new();
            response.DeletedProduct = _mapper.Map<ProductDto>(targetProduct);

            return Task.FromResult(response);
        }
    }
}
