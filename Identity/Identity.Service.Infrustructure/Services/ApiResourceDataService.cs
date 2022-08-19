using Ardalis.GuardClauses;
using Duende.IdentityServer.EntityFramework.Entities;
using Assets.Core.Identity.Service.Domain.Repository.Contracts;
using Assets.Core.Identity.Service.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Assets.Core.Identity.Service.Infrastructure.Services;
public sealed class ApiResourceDataService : IDataServiceWithRead<ApiResource>
{
    private readonly IBaseRepository<ApiResource> _apiResourcesRepository;
    public ApiResourceDataService(IBaseRepository<ApiResource> configurationDbContext)
    {
        _apiResourcesRepository = configurationDbContext ?? throw new ArgumentNullException(nameof(configurationDbContext));
    }

    public async Task<IList<ApiResource>> GetAsync()
    {
        IQueryable<ApiResource> apiResources = _apiResourcesRepository.GetAll()
                                                                    .Include(x => x.Properties)
                                                                    .Include(x => x.Scopes)
                                                                    .Include(x => x.Secrets)
                                                                    .Include(x => x.UserClaims);
                                                                        
        return await apiResources.ToListAsync().ConfigureAwait(false); 
    }

    public async Task AddAsync(ApiResource entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        var existingApiResource = await GetAsync(entity.Name,  true).ConfigureAwait(false);
        if (existingApiResource != null)
        {
            throw new InvalidOperationException(string.Format(ErrorMessage.ApiResourceAlreadyExist, entity.Name));
        }

        await ValidateScope(entity).ConfigureAwait(false);
        await _apiResourcesRepository.AddAsync(entity).ConfigureAwait(false);
    }

    public async Task UpdateAsync(ApiResource entity)
    {
        Guard.Against.Null(entity, nameof(entity));

        var existingApiResource = await GetAsync(entity.Name, true).ConfigureAwait(false);
        if (existingApiResource == null)
        {
            throw new InvalidOperationException(message: string.Format(ErrorMessage.ApiResourceMissing, entity.Name));

        }

        await ValidateScope(entity).ConfigureAwait(false);

        entity.Id = existingApiResource.Id;

        SetIdAndDetachExistingApiResourceSecret(existingApiResource, entity);

        SetIdAndDetachExistingUserClaims(existingApiResource, entity);

        SetIdAndDetachExistingAllowedScopes(existingApiResource, entity);

        SetIdAndDetachExistingProperties(existingApiResource, entity);

        _apiResourcesRepository.Detach(existingApiResource);

        await _apiResourcesRepository.UpdateAsync(entity);
    }

    public async Task<ApiResource> FindAsync(string identifier, bool includeRelatedData = false)
    {
        Guard.Against.NullOrWhiteSpace(identifier, nameof(identifier));

        return await GetAsync(identifier, includeRelatedData).ConfigureAwait(false);
    }

    private async Task<ApiResource> GetAsync(string name,  bool includeRelatedData = false)
    {
        var apiResources = _apiResourcesRepository.GetAll();

        if (includeRelatedData)
        {
            apiResources.Include(x => x.Properties)
                                     .Include(x => x.Scopes)
                                     .Include(x => x.Secrets)
                                     .Include(x => x.UserClaims);
        }
        var results = await apiResources.SingleOrDefaultAsync(x => x.Name == name).ConfigureAwait(false);
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
                _apiResourcesRepository.Detach(secret);
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

                _apiResourcesRepository.Detach(existingApiSecret);
            }
        }
    }

    private void SetIdAndDetachExistingProperties(ApiResource existingApiResource, ApiResource updatedApiResource)
    {
        if (existingApiResource.Properties == null || updatedApiResource.Properties == null)
        {
            return;
        }

        foreach (var property in updatedApiResource.Properties)
        {
            var existingProperty = existingApiResource.Properties.FirstOrDefault(x => x.Key == property.Key);
            if (existingProperty != null)
            {
                property.Id = existingProperty.Id;
                _apiResourcesRepository.Detach(existingProperty);
            }
        }
    }

    private void SetIdAndDetachExistingAllowedScopes(ApiResource existingApiResource, ApiResource updatedApiResource)
    {
        if (existingApiResource.Scopes == null || updatedApiResource.Scopes == null)
        {
            return;
        }

        foreach (var apiScope in updatedApiResource.Scopes)
        {
            var existingApiScope = existingApiResource.Scopes.FirstOrDefault(x => x.Scope == apiScope.Scope);

            if (existingApiScope != null)
            {
                apiScope.Id = existingApiScope.Id;
                _apiResourcesRepository.Detach(existingApiScope);
            }
        }
    }

    private void SetIdAndDetachExistingUserClaims(ApiResource existingApiResource, ApiResource updatedApiResource)
    {
        if (existingApiResource.UserClaims == null || updatedApiResource.UserClaims == null)
        {
            return;
        }

        foreach (var userClaim in updatedApiResource.UserClaims)
        {
            var existingUserClaim = existingApiResource.UserClaims.FirstOrDefault(x => x.Type == userClaim.Type);
            if (existingUserClaim != null)
            {
                userClaim.Id = existingUserClaim.Id;
                _apiResourcesRepository.Detach(existingUserClaim);
            }
        }
    }

    private async Task ValidateScope(ApiResource entity)
    {
        var identityResourcesName = await _apiResourcesRepository.GetAll().Select(x => x.Name).ToListAsync().ConfigureAwait(false);
        var resourceScopesName = entity.Scopes.Select(x => x.Scope);
        var duplicateScopes = identityResourcesName.Intersect(resourceScopesName, StringComparer.OrdinalIgnoreCase);
        if (duplicateScopes.Any())
        {
            throw new InvalidOperationException(string.Format(ErrorMessage.DuplicateScopes, string.Join(',', duplicateScopes)));
        }
    }
    }
