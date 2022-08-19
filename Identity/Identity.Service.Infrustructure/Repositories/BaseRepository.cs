using Assets.Core.Identity.Service.Infrastructure.Context;
using Assets.Core.Identity.Service.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Assets.Core.Identity.Service.Domain.Repository.Contracts;

namespace Assets.Core.Identity.Service.Infrastructure.Repositories;
public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
{
    protected readonly IdentityServerDbContext DbContext;
    protected readonly DbSet<TModel> DbSet;

    public BaseRepository(IdentityServerDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TModel>();
    }

    public async Task<TModel> AddAsync(TModel newEntity)
    {
        try
        {
            await DbSet.AddAsync(newEntity);
            await DbContext.SaveChangesAsync();
            return newEntity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<TModel> GetAsync(string entityId)
    {
        return await DbSet.FindAsync(entityId);
    }

    public async Task<TModel> UpdateAsync(TModel entity)
    {
        try
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new UnknownException("Please check details in inner exception", ex);
        }

        return entity;
    }

    public async Task DeleteAsync(int entityId)
    {
        var entity = await DbSet.FindAsync(entityId);
        if (entity != null)
        {
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }

    public IQueryable<TModel> GetAll()
    {
        return DbSet.AsNoTracking();
    }

    public void Detach<T>(T entity) 
    {
        DbContext.Entry(entity).State = EntityState.Detached;
    }
}