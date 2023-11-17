using Application.Abstractions.Persistence.Repositories.Users;
using Application.Features.Auths.Rules;
using Application.Utils.Hashing;
using Application.Utils.Hashing.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly AuthRuleManager _authRuleManager;
        private readonly IHashingTool _hashingTool;
        public RegisterCommandHandler(IUserWriteRepository userWriteRepository, AuthRuleManager authRuleManager, IHashingTool hashingTool = null)
        {
            _userWriteRepository = userWriteRepository;
            _authRuleManager = authRuleManager;
            _hashingTool = hashingTool;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _authRuleManager
                .UserMailMustNotBeDuplicate(request.Mail);

            HashResponse hashResponse = _hashingTool.GeneratePasswordHash(request.Password);
            User newUser = new();
            newUser.PasswordSalt = hashResponse.Salt;
            newUser.Mail = request.Mail;
            newUser.PasswordHash = hashResponse.Hash;

            await _userWriteRepository.AddAsync(newUser);

            RegisterCommandResponse response = new();
            return response;
        }
    }
}
