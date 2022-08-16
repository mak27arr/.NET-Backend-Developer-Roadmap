using Identity.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Helper.Extention
{
    public static class AuthHelper
    {
        public static void AuthConfig(this IServiceCollection service, AuthConfig config)
        {
            service.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                    IssuerSigningKey = config.IssuerSigningKey,
                    ValidateIssuer = config.ValidateIssuer,
                    ValidAudience = config.ValidAudience,
                    ValidIssuer = config.ValidIssuer,
                    ValidateAudience = config.ValidateAudience,
                    RequireExpirationTime = config.RequireExpirationTime,
                    ValidateLifetime = config.ValidateLifetime
                };
            });
        }
    }
}
