namespace Assets.Core.Identity.Service.Infrastructure.Services;
public interface IDataServiceWithRead<TEntity> : IDataService<TEntity>
{ 
    Task<IList<TEntity>> GetAsync();
}

