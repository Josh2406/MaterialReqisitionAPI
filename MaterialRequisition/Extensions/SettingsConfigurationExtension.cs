using MaterialRequisition.Application.Settings;

namespace MaterialRequisition.Extensions
{
    public static class SettingsConfigurationExtension
    {
        public static IServiceCollection ConfigureApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            return services; ;
        }
    }
}
