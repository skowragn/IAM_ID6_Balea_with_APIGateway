namespace Assets.Core.Identity.Service.Infrastructure.Services;

public interface IQuerableDataService<TEntity, TQuery> : IDataService<TEntity>
{
    Task<IList<TEntity>> GetAsync(TQuery query);
}

