using Assets.Core.Identity.Service.Infrastructure.Context;
using Assets.Core.Identity.Service.Domain.Entities;

namespace Assets.Core.Identity.Service.Infrastructure.Repositories;

public class ApplicationUserRepository : BaseRepository<ApplicationUser>
{
    public ApplicationUserRepository(IdentityServerDbContext dbContext) : base(dbContext)
    {
    }
}
