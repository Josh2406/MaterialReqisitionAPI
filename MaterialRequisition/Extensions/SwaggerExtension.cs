using Microsoft.OpenApi.Models;

namespace MaterialRequisition.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Contact = new OpenApiContact { Email = "ajibade.joshua.aj@gmail.com", Name = "Joshua Ajibade" },
                    Description = "Material Requisition API Documentation",
                    Title = "Material Requisition API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                var prefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "Material Requisition API Documentation");
            });

            return builder;
        }
    }
}
