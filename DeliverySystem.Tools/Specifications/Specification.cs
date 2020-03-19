using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.Tools.Specifications
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        protected abstract bool IsSatisfiedBy(TEntity entity);

        public virtual void EnforceRule(TEntity entity, string error)
        {
            if (!IsSatisfiedBy(entity))
            {
                throw new ValidationException(error);
            }
        }
    }
}
