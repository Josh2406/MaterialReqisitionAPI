using MaterialRequisition.Persistence.Context;

namespace MaterialRequisition.Persistence.ContextFactory
{
    public interface IDbContextFactory
    {
        RequisitionContext CreateDbContext();
    }
}
