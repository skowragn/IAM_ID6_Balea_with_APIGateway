namespace Assets.Core.Assets.Management.Api.Dependencies.Extensions;

public static class IdentityServerAuthenticationExtension
{
        public static IServiceCollection AddAuthenticationExt(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.RoleClaimType = "sub";
                });
            services.AddAuthorization(options =>
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                })
            );
            return services;
        }
    }