using System;

namespace DeliverySystem.Domain
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
            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;
        }

        protected void Update(int? updatedBy = null)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = updatedBy;
        }

        protected void Delete(int? deletedBy = null)
        {
            IsDeleted = true;
            Update(deletedBy);
        }
    }
}
