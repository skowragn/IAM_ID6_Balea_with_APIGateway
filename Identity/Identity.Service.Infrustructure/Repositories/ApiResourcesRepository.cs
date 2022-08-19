using Assets.Core.Identity.Service.Infrastructure.Context;
using EFEntities = Duende.IdentityServer.EntityFramework.Entities;

namespace Assets.Core.Identity.Service.Infrastructure.Repositories
{
    public class ApiResourcesRepository : BaseRepository<EFEntities.ApiResource>
    {
        public ApiResourcesRepository(IdentityServerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
