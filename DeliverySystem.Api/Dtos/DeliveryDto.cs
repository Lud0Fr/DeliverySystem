namespace DeliverySystem.Api.Dtos
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public AccessWindowDto AccessWindo { get; set; }
        public OderDto Order { get; set; }
        public RecipientDto Recipient { get; set; }
    }
}
