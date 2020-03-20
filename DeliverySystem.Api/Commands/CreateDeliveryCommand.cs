using DeliverySystem.Api.Dtos;
using MediatR;

namespace DeliverySystem.Api.Commands
{
    public class CreateDeliveryCommand : IRequest
    {
        public AccessWindowDto AccessWindow { get; set; }
        public RecipientDto Recipient { get; set; }
        public OderDto Order { get; set; }
        public int UserId { get; set; }
        public int PartnerId { get; set; }
    }
}
