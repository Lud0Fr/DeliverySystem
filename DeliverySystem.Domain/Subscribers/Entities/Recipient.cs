namespace DeliverySystem.Domain.Subscribers
{
    public class Recipient
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        private Recipient()
        { }

        public Recipient(
            string name,
            string address,
            string email,
            string phoneNumber)
        {
            Name = name;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
