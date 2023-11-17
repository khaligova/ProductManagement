using Application.Features.Auths.Commands.Register;
using FluentValidation;

namespace Application.Features.Auths.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Mail).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password)
               .NotEmpty()
               .NotNull()
               .MinimumLength(8)
               .Must(ContainsNumber).WithMessage("Password contains at least 1 number")
               .Must(ContainsCapitalLetter).WithMessage("Password contains at least capital letter");
        }

        public bool ContainsNumber(string password)
        {
            foreach (char symbol in password)
            {
                int altCode = (int)symbol;
                //48 is '0' symbol's altcode and 57 is '9'
                if (altCode < 48 || altCode > 57)
                    return true;
            }

            return false;
        }

        public bool ContainsCapitalLetter(string password)
        {
            string lowerPassword = password.ToLower();

            if (lowerPassword == password)
                return false;

            return true;
        }
    }
}
