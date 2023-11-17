using Application.Abstractions.Persistence.Repositories.Products;
using Application.Features.Products.Dtos;
using Application.Features.Products.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ProductRuleManager _productRuleManager;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, ProductRuleManager productRuleManager, IMapper mapper)
        {
            _productWriteRepository = productWriteRepository;
            _productRuleManager = productRuleManager;
            _mapper = mapper;
        }

        public Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product targetProduct;

            _productRuleManager
                .CheckIfProductExist(request.Product.Id, out targetProduct);

            targetProduct = _mapper.Map(request.Product,targetProduct);

            Product updatedProdcut=_productWriteRepository.Update(targetProduct);

            UpdateProductCommandResponse response = new();
            response.UpdatedProduct = _mapper.Map<ProductDto>(updatedProdcut);

            return Task.FromResult(response);
        }
    }

}
