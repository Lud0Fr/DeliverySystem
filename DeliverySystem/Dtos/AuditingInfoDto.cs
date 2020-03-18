using System;

namespace DeliverySystem.Api.Dtos
{
    public class AuditingInfoDto
    {
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
