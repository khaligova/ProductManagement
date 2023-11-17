using Application.Abstractions.Persistence.Repositories.Users;
using Application.Features.Auths.Rules;
using Application.Utils.JWT;
using Application.Utils.JWT.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auths.Commands.Login
{
    public partial class LoginCommand
    {
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthRuleManager _authRuleManager;

            public LoginCommandHandler(IUserReadRepository userReadRepository, ITokenHelper tokenHelper, AuthRuleManager authRuleManager)
            {
                _userReadRepository = userReadRepository;
                _tokenHelper = tokenHelper;
                _authRuleManager = authRuleManager;
            }

            public Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user;

                _authRuleManager
                    .DoesUserEmailExist(request.Mail, out user)
                    .CheckUserCredentials(user,request.Password);

                List<string> claims = user.UserClaims.Select(uc => uc.Claim.Name).ToList();
                Token token = _tokenHelper.CreateToken(user.Mail,claims);

                LoginCommandResponse response = new LoginCommandResponse(token.AccessToken);

                return Task.FromResult(response);

            }
        }

    }
}
