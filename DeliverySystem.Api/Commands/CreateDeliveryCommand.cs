using DeliverySystem.Api.Dtos;
using MediatR;

namespace DeliverySystem.Api.Commands
{
    public class CreateDeliveryCommand : IRequest<int>
    {
        public AccessWindowDto AccessWindow { get; set; }
        public RecipientDto Recipient { get; set; }
        public OrderDto Order { get; set; }
        public int UserId { get; set; }
        public int PartnerId { get; set; }
    }
}
