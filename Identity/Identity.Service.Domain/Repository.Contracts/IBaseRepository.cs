namespace Assets.Core.Identity.Service.Domain.Repository.Contracts;
public interface IBaseRepository<T>
{
    Task<T> AddAsync(T newEntity);
    Task<T> GetAsync(string entityId);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(int entityId);
    IQueryable<T> GetAll();
    void Detach<T>(T entity);
}