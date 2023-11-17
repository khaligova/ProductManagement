using Application.Abstractions.Persistence.Repositories.Users;
using Application.CrossCuttingConcerns.Exceptions.Types;
using Application.Utils.Hashing;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthRuleManager
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IHashingTool _hashingTool;


        public AuthRuleManager(IUserReadRepository userReadRepository, IHashingTool hashingTool)
        {
            _userReadRepository = userReadRepository;
            _hashingTool = hashingTool;
        }

        public AuthRuleManager DoesUserEmailExist(string email, out User user)
        {
            user = _userReadRepository.Get(predicate: user => user.Mail == email,
                                          include: u => u.Include(u => u.UserClaims).ThenInclude(uc => uc.Claim)
                                          ).FirstOrDefault();

            if (user is null)
                throw new AuthenticationException("Your email or passwor is wrong");

            return this;
        }

        public AuthRuleManager CheckUserCredentials(User user, string password)
        {
            if (!_hashingTool.VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash))
                throw new AuthenticationException("Your login credentials are not valid.");

            return this;
        }

        public AuthRuleManager UserMailMustNotBeDuplicate(string mail)
        {
            if (_userReadRepository.Get(predicate: x => x.Mail == mail).Any())
                throw new BusinessException("This mail already exist.");

            return this;
        }
    }
}
