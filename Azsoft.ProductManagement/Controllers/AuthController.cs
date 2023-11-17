using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.Register;
using Application.Filters.Authorization;
using Azsoft.ProductManagement.Controllers.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Azsoft.ProductManagement.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost]
        [IgnoreAuth]
        public IActionResult Login([FromBody] LoginCommand command)
        {
            var result = Mediatr.Send(command).Result;
            return Ok(result);
        }

        [HttpPost]
        [IgnoreAuth]
        public IActionResult Register([FromBody] RegisterCommand command)
        {
            var result = Mediatr.Send(command).Result;
            return Ok(result);
        }
    }
}
