using System;

namespace DeliverySystem.Api.Dtos
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public AccessWindowDto AccessWindow { get; set; }
        public OrderDto Order { get; set; }
        public RecipientDto Recipient { get; set; }
        public int UserId { get; set; }
        public int PartnerId { get; set; }
        public DeliveryStateDto State { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
