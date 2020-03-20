namespace DeliverySystem.Api.Dtos
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public AccessWindowDto AccessWindo { get; set; }
        public OrderDto Order { get; set; }
        public RecipientDto Recipient { get; set; }
    }
}
