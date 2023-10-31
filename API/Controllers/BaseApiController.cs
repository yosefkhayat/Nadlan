using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// This class is the base for all controllers.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices
            .GetService<IMediator>();
    }
}
