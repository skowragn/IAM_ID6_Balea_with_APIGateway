using Duende.IdentityServer.EntityFramework.Interfaces;

namespace Assets.Core.Identity.Service.Infrastructure.Context;

    public interface IIdentityServerDbContext : IPersistedGrantDbContext, IConfigurationDbContext
    {
    }