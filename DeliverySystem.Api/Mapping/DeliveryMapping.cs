using AutoMapper;
using DeliverySystem.Api.Dtos;
using DeliverySystem.Domain.Deliveries;

namespace DeliverySystem.Api.Mapping
{
    public class DeliveryMapping : Profile
    {
        public DeliveryMapping()
        {
            CreateMap<Delivery, DeliveryDto>();
            CreateMap<AccessWindow, AccessWindowDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Recipient, RecipientDto>();
        }
    }
}
