using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand:IRequest<RegisterCommandResponse>
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
