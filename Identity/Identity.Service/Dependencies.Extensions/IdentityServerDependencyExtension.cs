using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Assets.Core.Identity.Service.Infrastructure.Context;
using Assets.Core.Identity.Service.Domain.Entities;
using System.Security.Cryptography.X509Certificates;
using Assets.Core.Identity.Service.Constants;
using Assets.Core.Identity.Service.Models.Config;
using Duende.IdentityServer;
using Rsk.AspNetCore.Authentication.Saml2p;

namespace Assets.Core.Identity.Service.Dependencies.Extensions;

public static class IdentityServerDependencyExtension
{
    public static IServiceCollection UseIdentityServerDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var connectionString = configuration.GetConnectionString("sqlConnection");

        services.AddDbContext<IdentityServerDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityServerDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }

    public static IIdentityServerBuilder UseIdentityServerBuilder(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var connectionString = configuration.GetConnectionString("sqlConnection");

        var builder = services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            options.EmitStaticAudienceClaim = true;
            options.UserInteraction.LoginUrl = new PathString("/Account/Login");
            options.UserInteraction.LogoutUrl = new PathString("/Account/Logout");
        })
        .AddConfigurationStore<IdentityServerDbContext>(options =>
        {
            options.ConfigureDbContext = b => b.UseSqlServer(connectionString);
        })
        .AddOperationalStore<IdentityServerDbContext>(options =>
        {
            options.ConfigureDbContext = b => b.UseSqlServer(connectionString);
        })
        .AddAspNetIdentity<ApplicationUser>();
        
        // not recommended for production - you need to store your key material somewhere secure
        builder.AddDeveloperSigningCredential();

        return builder;
    }

    public static IServiceCollection AddExternalAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        var externalProvidersConfig = configuration.GetSection(ConfigurationConstants.ExternalProvidersConfiguration)
                                        .Get<ExternalProvidersConfiguration>();

        var samlExtProvider = externalProvidersConfig.SAML2PExternalIdentityProvider;
        var googleExtProvider = externalProvidersConfig.GoogleExternalIdentityProvider;

        if (samlExtProvider != null && googleExtProvider != null)
        {

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google

                    if (googleExtProvider.ClientId != null) options.ClientId = googleExtProvider.ClientId;
                    if (googleExtProvider.ClientSecret != null) options.ClientSecret = googleExtProvider.ClientSecret;
                    options.CallbackPath = googleExtProvider.CallbackPath;
                })
                .AddSaml2p(samlExtProvider.ProviderName, options =>
                {
                    options.Licensee = samlExtProvider.Licensee;
                    options.LicenseKey = samlExtProvider.LicenseKey;

                    options.NameIdClaimType = samlExtProvider.NameIdClaimType;
                    options.CallbackPath = samlExtProvider.CallbackPath;
                    options.SignInScheme = samlExtProvider.SignInScheme;

                    options.IdentityProviderOptions = new IdpOptions()
                    {
                        EntityId = samlExtProvider.IdentityProviderOptionsEntityId,
                        SigningCertificates = new List<X509Certificate2> {new("idsrv3test.cer")},
                        SingleSignOnEndpoint = new SamlEndpoint(samlExtProvider.IdentityProviderOptionsSsoEndpoint, SamlBindingTypes.HttpRedirect),
                        SingleLogoutEndpoint = new SamlEndpoint(samlExtProvider.IdentityProviderOptionsSloEndpoint, SamlBindingTypes.HttpRedirect)
                    };

                    options.ServiceProviderOptions = new SpOptions()
                    {
                        EntityId = samlExtProvider.ServiceProviderOptionsEntityId,
                        MetadataPath = samlExtProvider.ServiceProviderOptionsMetadataPath
                    };
                });
        }

        return services;
    }
}