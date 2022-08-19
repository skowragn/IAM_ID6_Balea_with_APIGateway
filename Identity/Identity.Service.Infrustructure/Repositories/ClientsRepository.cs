using Assets.Core.Identity.Service.Infrastructure.Context;
using EFEntities = Duende.IdentityServer.EntityFramework.Entities;

namespace Assets.Core.Identity.Service.Infrastructure.Repositories;

public class ClientsRepository : BaseRepository<EFEntities.Client>
{
    public ClientsRepository(IdentityServerDbContext dbContext) : base(dbContext)
    {
    }
}

