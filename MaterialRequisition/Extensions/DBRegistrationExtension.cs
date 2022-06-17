using MaterialRequisition.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MaterialRequisition.Extensions
{
    public static class DBRegistrationExtension
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RequisitionContext>(options => options.UseSqlServer(configuration.GetConnectionString("RequisitionConnection"),
                options =>
                {
                    options.CommandTimeout(20);
                }));
            return services;
        }
    }
}
