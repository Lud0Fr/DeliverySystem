using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliverySystem.Api.Controllers
{
    public class ApiControllerBase : Controller
    {
        private readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<TResponse> HandleAsync<TResponse>(IRequest<TResponse> command)
        {
            return await _mediator.Send(command);
        }
    }
}
