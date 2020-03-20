using DeliverySystem.Api.Commands;
using DeliverySystem.Api.Queries;
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
        private readonly IDeliveryQueries _deliveryQuery;

        public DeliveryController(
            IMediator mediator,
            IDeliveryQueries deliveryQuery)
            : base(mediator)
        {
            _deliveryQuery = deliveryQuery;
        }

        [HttpGet()]
        [AuthorizeJwtRole(RoleName.Admin, RoleName.Partner, RoleName.UserConsumerMarket)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _deliveryQuery.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [AuthorizeJwtRole(RoleName.Admin, RoleName.Partner, RoleName.UserConsumerMarket)]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            return Ok(await _deliveryQuery.GetAsync(id));
        }

        [HttpPost]
        [AuthorizeJwtRole(RoleName.Admin)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateDeliveryCommand command)
        {
            return Ok(await HandleAsync(command));
        }

        [HttpPut("{deliveryId:int}/approve")]
        [AuthorizeJwtRole(RoleName.UserConsumerMarket)]
        public async Task<IActionResult> ApproveAsync([FromRoute]int deliveryId)
        {
            await HandleAsync(new ApproveDeliveryCommand(deliveryId));

            return NoContent();
        }

        [HttpPut("{deliveryId:int}/complete")]
        [AuthorizeJwtRole(RoleName.Partner)]
        public async Task<IActionResult> CompleteAsync([FromRoute]int deliveryId)
        {
            await HandleAsync(new CompleteDeliveryCommand(deliveryId));

            return NoContent();
        }

        [HttpPut("{deliveryId:int}/cancel")]
        [AuthorizeJwtRole(RoleName.Partner, RoleName.UserConsumerMarket)]
        public async Task<IActionResult> CancelAsync([FromRoute]int deliveryId)
        {
            await HandleAsync(new CancelDeliveryCommand(deliveryId));

            return NoContent();
        }

        [HttpDelete("{deliveryId:int}")]
        [AuthorizeJwtRole(RoleName.Admin)]
        public async Task<IActionResult> DeleteAsync([FromRoute]int deliveryId)
        {
            await HandleAsync(new DeleteDeliveryCommand(deliveryId));

            return NoContent();
        }
    }
}
