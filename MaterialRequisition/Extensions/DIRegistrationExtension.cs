using AutoMapper;
using MaterialRequisition.Application.Interfaces;
using MaterialRequisition.Business.Automapper;
using MaterialRequisition.Business.Implementations;
using MaterialRequisition.Business.Implementations.Repository;
using MaterialRequisition.Persistence.ContextFactory;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MaterialRequisition.Extensions
{
    public static class DIRegistrationExtension
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            //Configure Automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //Register IMemoryCache
            services.AddMemoryCache();

            //Register HttpContextAccessor
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Register DbContextFactory
            services.AddScoped<IDbContextFactory, DbContextFactory>();

            //Register Service Contracts and their Implementations
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<ICachingService, CachingService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IBusinessUnitService, BusinessUnitService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRequisitionService, RequsitionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStockPostingService, StockPostingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
