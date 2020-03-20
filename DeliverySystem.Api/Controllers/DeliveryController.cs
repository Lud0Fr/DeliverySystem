﻿using DeliverySystem.Api.Commands;
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
    }
}
