namespace Assets.Core.Identity.Service.Infrastructure.Services;
public interface IDataService<TEntity>
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> FindAsync(string identifier, bool includeRelatedData = false);
}
