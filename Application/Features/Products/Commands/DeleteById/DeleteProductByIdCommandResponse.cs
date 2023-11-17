using Application.Features.Products.Dtos;

namespace Application.Features.Products.Commands.DeleteById
{
    public class DeleteProductByIdCommandResponse
    {
        public ProductDto DeletedProduct{ get; set; }
    }
}
