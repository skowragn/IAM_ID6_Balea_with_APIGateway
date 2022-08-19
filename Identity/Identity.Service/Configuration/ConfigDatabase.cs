using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.Mappers;
using Assets.Core.Identity.Service.Infrastructure.Context;

namespace Assets.Core.Identity.Service.Configuration;

public static class ConfigDatabase
{
    public static void InitDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();

        context.Database.Migrate();

        if (!context.Clients.Any())
        {
            foreach (var client in SeedClients.Clients)
                context.Clients.Add(client.ToEntity());

            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in SeedClients.IdentityResources)
                context.IdentityResources.Add(resource.ToEntity());

            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in SeedClients.ApiScopes)
                context.ApiScopes.Add(resource.ToEntity());

            context.SaveChanges();
        }
    }
}