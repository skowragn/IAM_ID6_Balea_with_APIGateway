using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Options;
using System.Reflection;
using Assets.Core.Identity.Service.Domain.Entities;

namespace Assets.Core.Identity.Service.Infrastructure.Context
{
    public class IdentityServerDbContext : IdentityDbContext<ApplicationUser>, IIdentityServerDbContext
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<IdentityProvider> IdentityProviders { get; set; }
        public DbSet<ServerSideSession> ServerSideSessions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<PushedAuthorizationRequest> PushedAuthorizationRequests { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigurePersistentGrantDbContext(builder);
            ConfigureConfigurationDbContext(builder);
        }

        private static void ConfigurePersistentGrantDbContext(ModelBuilder builder)
        {
            var options = new OperationalStoreOptions();
            SetSchemaForAllTables(options, "idsGrants");

            builder.ConfigurePersistedGrantContext(options);
        }

        private static void ConfigureConfigurationDbContext(ModelBuilder builder)
        {
            var options = new ConfigurationStoreOptions();
            SetSchemaForAllTables(options, "idsconfig");

            builder.ConfigureClientContext(options);
            builder.ConfigureResourcesContext(options);
        }

        private static void SetSchemaForAllTables<T>(T options, string schema)
        {
            var tableConfigurationType = typeof(TableConfiguration);
            var schemaProperty = tableConfigurationType.GetProperty(nameof(TableConfiguration.Schema));

            var tableConfigurations = options.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => tableConfigurationType.IsAssignableFrom(property.PropertyType))
                .Select(property => property.GetValue(options, null));

            foreach (var table in tableConfigurations)
                schemaProperty.SetValue(table, schema, null);
        }

    }
}
