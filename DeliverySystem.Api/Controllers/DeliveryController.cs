using DeliverySystem.Api.Commands;
using DeliverySystem.Tools.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliverySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ApiControllerBase
    {
        public DeliveryController(IMediator mediator)
            : base(mediator)
        { }

        [HttpPost]
        [AuthorizeJwtRole(RoleName.Admin)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateDeliveryCommand command)
        {
            await HandleAsync(command);

            return NoContent();
        }
    }
}
