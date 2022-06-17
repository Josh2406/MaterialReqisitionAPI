using MaterialRequisition.Application.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MaterialRequisition.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              // Adding Jwt Bearer  
              .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(JWTSecret.Key))
                  };
              });

            return services;
        }
    }
}
