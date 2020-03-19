namespace DeliverySystem.Tools
{
    public class JwtConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public int TokenExpiresInDays { get; set; }
        public string TokenExpiresNotBefore { get; set; }
        public bool ValidateActor { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }
}
