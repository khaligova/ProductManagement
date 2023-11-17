using Application.CrossCuttingConcerns.Exceptions.Types;
using Application.Utils.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public partial class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string Mail { get; set; }
        public string Password { get; set; }

    }
}
