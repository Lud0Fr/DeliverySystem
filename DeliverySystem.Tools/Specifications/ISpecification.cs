namespace DeliverySystem.Tools.Specifications
{
    public interface ISpecification<TEntity>
    {
        void EnforceRule(TEntity entity, string error);
    }
}
