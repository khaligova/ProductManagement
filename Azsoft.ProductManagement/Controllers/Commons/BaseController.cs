using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Azsoft.ProductManagement.Controllers.Commons
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        public IMediator Mediatr => _mediatr ?? HttpContext.RequestServices.GetService<IMediator>();
        private IMediator? _mediatr;
    }
}
