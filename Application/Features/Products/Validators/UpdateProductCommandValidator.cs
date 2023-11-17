using Application.Features.Products.Commands.Update;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Product.Name).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(p => p.Product.Price).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(p => p.Product.Id).NotEmpty().NotNull();
        }
    }
}
