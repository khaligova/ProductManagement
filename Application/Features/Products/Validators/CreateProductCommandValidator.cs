using Application.Features.Products.Commands.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Validators
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(p => p.Price).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
