using DeliverySystem.Api.Commands;
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

        // TODO add authorization
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateDeliveryCommand command)
        {
            await HandleAsync(command);

            return NoContent();
        }
    }
}
