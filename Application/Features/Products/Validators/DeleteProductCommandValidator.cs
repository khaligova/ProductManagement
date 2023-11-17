using Application.Features.Products.Commands.DeleteById;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductByIdCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
        }
    }
}
