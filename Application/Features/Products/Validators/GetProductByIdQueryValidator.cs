using Application.Features.Products.Queries.GetById;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
        }
    }
}
