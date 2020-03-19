using DeliverySystem.Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliverySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ApiControllerBase
    {
        public IdentityController(IMediator mediator)
            : base(mediator)
        { }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody]SignInCommand command)
        {
            return Ok(await HandleAsync(command));
        }
    }
}
