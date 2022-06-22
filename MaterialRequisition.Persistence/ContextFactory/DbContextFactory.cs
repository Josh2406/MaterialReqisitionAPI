using MaterialRequisition.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MaterialRequisition.Persistence.ContextFactory
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration _configuration;

        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RequisitionContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<RequisitionContext>()
                .UseSqlServer(_configuration.GetConnectionString("RequisitionConnection"),
                 sqlServerOptionsAction: sqlOptions =>
                 {
                     sqlOptions.EnableRetryOnFailure
                     (
                         maxRetryCount: 10,
                         maxRetryDelay: TimeSpan.FromSeconds(10),
                         errorNumbersToAdd: null
                      );
                 })
                .Options;

            return new RequisitionContext(options);
        }
    }
}
