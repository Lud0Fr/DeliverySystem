using DeliverySystem.Tools.Specifications;

namespace DeliverySystem.Domain.Deliveries
{
    #region Interface

    public interface IExistsDeliverySpecification : ISpecification<Delivery>
    {
    }

    #endregion

    public class ExistsDeliverySpecification :
        Specification<Delivery>,
        IExistsDeliverySpecification
    {
        protected override bool IsSatisfiedBy(Delivery entity)
        {
            return entity != null;
        }
    }
}
