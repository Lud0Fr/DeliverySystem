using DeliverySystem.Tools.Specifications;

namespace DeliverySystem.Domain.Identities
{
    #region Interface

    public interface IExistsIdentitySpecification : ISpecification<Identity>
    {
    }

    #endregion

    public class ExistsIdentitySpecification :
        Specification<Identity>,
        IExistsIdentitySpecification
    {
        protected override bool IsSatisfiedBy(Identity entity)
        {
            return entity != null;
        }
    }
}
