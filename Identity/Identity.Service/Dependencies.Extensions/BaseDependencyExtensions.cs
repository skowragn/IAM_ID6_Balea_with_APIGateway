using Assets.Core.Identity.Service.Constants;
using Assets.Core.Identity.Service.Models.Config;
using Assets.Core.Identity.Service.Services;
using Duende.IdentityServer.Services;

namespace Assets.Core.Identity.Service.Dependencies.Extensions;

    public static class BaseDependencyExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExternalProvidersConfiguration>(options =>
                configuration.GetSection(ConfigurationConstants.ExternalProvidersConfiguration).Bind(options));
            return services;
        }

        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IProfileService, ProfileService>();
            return services;
        }
    }