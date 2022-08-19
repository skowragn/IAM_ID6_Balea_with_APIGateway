using Ardalis.GuardClauses;
using Duende.IdentityServer.EntityFramework.Entities;
using Assets.Core.Identity.Service.Domain.Repository.Contracts;
using Assets.Core.Identity.Service.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Assets.Core.Identity.Service.Domain.Entities;

namespace Assets.Core.Identity.Service.Infrastructure.Services;

public sealed class ApplicationUserDataService : IDataServiceWithRead<ApplicationUser>
{
    private readonly IBaseRepository<ApplicationUser> _applicationUserRepository;
    public ApplicationUserDataService(IBaseRepository<ApplicationUser> applicationUserRepository)
    {
        _applicationUserRepository = applicationUserRepository ?? throw new ArgumentNullException(nameof(applicationUserRepository));
    }

    public async Task<IList<ApplicationUser>> GetAsync()
    {
        IQueryable<ApplicationUser> appUsers = _applicationUserRepository.GetAll()
                                                                        .Include(x => x.UserName)
                                                                        .Include(x => x.Email)
                                                                        .Include(x => x.EmailConfirmed)
                                                                        .Include(x => x.Description)
                                                                        .Include(x => x.ValidFrom)
                                                                        .Include(x => x.ValidTo)
                                                                        .Include(x => x.Disabled)
                                                                        .Include(x => x.HsaId)
                                                                        .Include(x => x.NetworkUserId)
                                                                        .Include(x => x.Ssn)
                                                                        .Include(x => x.VrkId)
                                                                        .Include(x => x.DomainId);

        return await appUsers.ToListAsync().ConfigureAwait(false);
    }

    public async Task AddAsync(ApplicationUser entity)
    {
        Guard.Against.Null(entity, nameof(entity));

        var existingApiResource = await GetAsync(entity.UserName, true).ConfigureAwait(false);
        if (existingApiResource != null)
        {
            throw new InvalidOperationException(string.Format(ErrorMessage.ApiResourceAlreadyExist, entity.UserName));
        }

        await ValidateScope(entity).ConfigureAwait(false);
        await _applicationUserRepository.AddAsync(entity).ConfigureAwait(false);
    }

    public async Task UpdateAsync(ApplicationUser entity)
    {
        Guard.Against.Null(entity, nameof(entity));

        var existingAppUser = await GetAsync(entity.UserName, true).ConfigureAwait(false);
        
        if (existingAppUser == null)
        {
            throw new InvalidOperationException(message: string.Format(ErrorMessage.ApiResourceMissing, entity.UserName));

        }

        await ValidateScope(entity).ConfigureAwait(false);

        entity.Id = existingAppUser.Id;

        _applicationUserRepository.Detach(existingAppUser);

        await _applicationUserRepository.UpdateAsync(entity);
    }

    private async Task<ApplicationUser> GetAsync(string name, bool includeRelatedData = false)
    {
        var apiResources = _applicationUserRepository.GetAll();

        if (includeRelatedData)
        {
            apiResources.Include(x => x.UserName)
                                     .Include(x => x.Ssn)
                                     .Include(x => x.Email)
                                     .Include(x => x.DomainId);
        }
        var results = await apiResources.SingleOrDefaultAsync(x => x.UserName == name).ConfigureAwait(false);
        return results;
    }

    private void SetIdAndDetachExistingApiResourceSecret(ApiResource existingApiResource, ApiResource updatedApiResource)
    {
        if (existingApiResource.Secrets == null)
        {
            return;
        }

        if (updatedApiResource.Secrets == null || !updatedApiResource.Secrets.Any())
        {
            existingApiResource.Secrets.ForEach(secret =>
            {
                _applicationUserRepository.Detach(secret);
            });

            updatedApiResource.Secrets = existingApiResource.Secrets;
        }

        foreach (var secret in updatedApiResource.Secrets)
        {
            var existingApiSecret = existingApiResource.Secrets.FirstOrDefault(x => x.Value == secret.Value
                                                                                 && x.Type == secret.Type);
            if (existingApiSecret != null)
            {
                secret.Id = existingApiSecret.Id;

                _applicationUserRepository.Detach(existingApiSecret);
            }
        }
    }


    private async Task ValidateScope(ApplicationUser entity)
    {
        var appUserName = await _applicationUserRepository.GetAll().Select(x => x.UserName).ToListAsync().ConfigureAwait(false);
    }

    Task<ApplicationUser> IDataService<ApplicationUser>.FindAsync(string identifier, bool includeRelatedData)
    {
        throw new NotImplementedException();
    }
}