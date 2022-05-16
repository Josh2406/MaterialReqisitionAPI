using Microsoft.EntityFrameworkCore;

namespace MaterialRequisition.Persistence.Context
{
    public class RequisitionContext: DbContext
    {
        public RequisitionContext(DbContextOptions<RequisitionContext> dbContextOptions): base(dbContextOptions) { }
    }
}
