using System;

namespace DeliverySystem.Tools.Domain
{
    public class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public int? CreatedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public int? UpdatedBy { get; protected set; }
        public bool IsDeleted { get; protected set; }

        protected Entity() { }

        protected void New(int? createdBy = null)
        {
            CreatedBy = createdBy;
        }

        protected void Update(int? updatedBy = null)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        public void Delete(int? deletedBy = null)
        {
            IsDeleted = true;
            Update(deletedBy);
        }
    }
}
