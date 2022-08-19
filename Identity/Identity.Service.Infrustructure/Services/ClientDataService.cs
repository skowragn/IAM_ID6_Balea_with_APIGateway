using Ardalis.GuardClauses;
using Assets.Core.Identity.Service.Infrastructure.Models;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Assets.Core.Identity.Service.Domain.Constants;
using EFEntities = Duende.IdentityServer.EntityFramework.Entities;
using Assets.Core.Identity.Service.Domain.Repository.Contracts;

namespace Assets.Core.Identity.Service.Infrastructure.Services;

public sealed class ClientDataService : IQuerableDataService<EFEntities.Client, ClientQuery>
{
    private readonly IBaseRepository<EFEntities.Client> _clientDataRepository;

    public ClientDataService(IBaseRepository<EFEntities.Client> clientDataRepository)
    {
        _clientDataRepository = clientDataRepository;
    }

    public async Task<IList<Client>> GetAsync(ClientQuery query)
    {
        IQueryable<Client> clients = _clientDataRepository.GetAll()
                                                        .Include(x => x.AllowedGrantTypes)
                                                        .Include(x => x.RedirectUris)
                                                        .Include(x => x.PostLogoutRedirectUris)
                                                        .Include(x => x.AllowedScopes)
                                                        .Include(x => x.ClientSecrets)
                                                        .Include(x => x.Claims)
                                                        .Include(x => x.IdentityProviderRestrictions)
                                                        .Include(x => x.AllowedCorsOrigins)
                                                        .Include(x => x.Properties);

        if (!string.IsNullOrEmpty(query.ClaimValue))
        {
            var claimType = string.IsNullOrEmpty(query.ClaimType) ? "claim" : query.ClaimType;
            clients = clients.Where(c => c.Claims.Any(cl => cl.Type == claimType && cl.Value == query.ClaimValue));
        }

        return await clients.ToListAsync().ConfigureAwait(false);
    }

    public async Task AddAsync(Client entity)
    {
        Guard.Against.Null(entity, nameof(entity));

        var existingClient = await GetAsync(entity.ClientId).ConfigureAwait(false);
        if (existingClient != null)
        {
            throw new InvalidOperationException(string.Format(ErrorMessage.ClientAlreadyExist, entity.ClientId));
        }

        await _clientDataRepository.AddAsync(entity).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Client entity)
    {
        Guard.Against.Null(entity, nameof(entity));

        var existingClient = await GetAsync(entity.ClientId, true).ConfigureAwait(false);

        if (existingClient == null)
        {
            throw new InvalidOperationException(string.Format(ErrorMessage.ClientMissing, entity.ClientId));
        }

        entity.Id = existingClient.Id;
        UpdateClientProperties(entity, existingClient);

        _clientDataRepository.Detach(existingClient);
        await _clientDataRepository.UpdateAsync(entity);

    }

    public async Task<Client> FindAsync(string identifier, bool includeRelatedData = false)
    {
        Guard.Against.NullOrWhiteSpace(identifier, nameof(identifier));

        return await GetAsync(identifier, includeRelatedData).ConfigureAwait(false);
    }

    private async Task<Client> GetAsync(string clientId, bool includeRelatedData = false)
    {
        if (includeRelatedData)
        {
            return await _clientDataRepository.GetAsync(clientId);
        }

        return new Client();
    }

    private void UpdateClientProperties(Client entity, Client existingClient)
    {
        SetIdAndDetachExistingClaims(existingClient, entity);

        SetIdAndDetachExistingAllowedCorsOrigins(existingClient, entity);

        SetIdAndDetachExistingAllowedGrantTypes(existingClient, entity);

        SetIdAndDetachExistingIdPRestrictions(existingClient, entity);

        SetIdAndDetachExistingPostLogoutRedirectUris(existingClient, entity);

        SetIdAndDetachExistingProperties(existingClient, entity);

        SetIdAndDetachExistingRedirectUris(existingClient, entity);

        SetIdAndDetachExistingAllowedScopes(existingClient, entity);

        SetIdAndDetachExistingClientSecrets(existingClient, entity);

        SetIdAndDetachExistingDebugRedirectUris(existingClient, entity);
    }

    private void SetIdAndDetachExistingAllowedScopes(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.AllowedScopes == null || clientToUpdate.AllowedScopes == null)
        {
            return;
        }

        foreach (var clientScope in clientToUpdate.AllowedScopes)
        {
            var existingClientScope = existingClient.AllowedScopes.FirstOrDefault(x => x.Scope == clientScope.Scope);

            if (existingClientScope != null)
            {
                clientScope.Id = existingClientScope.Id;
                _clientDataRepository.Detach(existingClientScope);
            }
        }
    }

    private void SetIdAndDetachExistingProperties(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.Properties == null || clientToUpdate.Properties == null)
        {
            return;
        }

        foreach (var property in clientToUpdate.Properties)
        {
            var existingProperty = existingClient.Properties.FirstOrDefault(x => x.Key == property.Key);

            if (existingProperty != null)
            {
                property.Id = existingProperty.Id;
                _clientDataRepository.Detach(existingProperty);
            }
        }
    }

    private void SetIdAndDetachExistingAllowedGrantTypes(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.AllowedGrantTypes == null || clientToUpdate.AllowedGrantTypes == null)
        {
            return;
        }

        foreach (var grantType in clientToUpdate.AllowedGrantTypes)
        {
            var existingAllowedGrantType = existingClient.AllowedGrantTypes.FirstOrDefault(x => x.GrantType == grantType.GrantType);

            if (existingAllowedGrantType != null)
            {
                grantType.Id = existingAllowedGrantType.Id;
                _clientDataRepository.Detach(existingAllowedGrantType);
            }
        }
    }

    private void SetIdAndDetachExistingClientSecrets(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.ClientSecrets == null)
        {
            return;
        }

        if (clientToUpdate.ClientSecrets == null || !clientToUpdate.ClientSecrets.Any())
        {
            existingClient.ClientSecrets.ForEach(secret =>
                {
                    _clientDataRepository.Detach(secret);
                });

            clientToUpdate.ClientSecrets = existingClient.ClientSecrets;
            return;
        }

        foreach (var secret in clientToUpdate.ClientSecrets)
        {
            var existingClientSecret = existingClient.ClientSecrets.FirstOrDefault(x => x.Value == secret.Value && x.Type == secret.Type);

            if (existingClientSecret != null)
            {
                secret.Id = existingClientSecret.Id;

                _clientDataRepository.Detach(existingClientSecret);
            }
        }
    }
    private void SetIdAndDetachExistingClaims(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.Claims == null || clientToUpdate.Claims == null)
        {
            return;
        }

        foreach (var claim in clientToUpdate.Claims)
        {
            var existingClaim = existingClient.Claims.FirstOrDefault(x => x.Type == claim.Type);
            if (existingClaim != null)
            {
                claim.Id = existingClaim.Id;
                _clientDataRepository.Detach(existingClaim);
            }
        }
    }

    private void SetIdAndDetachExistingAllowedCorsOrigins(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.AllowedCorsOrigins == null || clientToUpdate.AllowedCorsOrigins == null)
        {
            return;
        }

        foreach (var corsOrigin in clientToUpdate.AllowedCorsOrigins)
        {
            var existingCorsOrigin = existingClient.AllowedCorsOrigins.FirstOrDefault(x => x.Origin == corsOrigin.Origin);
            if (existingCorsOrigin != null)
            {
                corsOrigin.Id = existingCorsOrigin.Id;
                _clientDataRepository.Detach(existingCorsOrigin);
            }
        }
    }

    private void SetIdAndDetachExistingIdPRestrictions(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.IdentityProviderRestrictions == null || clientToUpdate.IdentityProviderRestrictions == null)
        {
            return;
        }

        foreach (var identityProviderRestriction in clientToUpdate.IdentityProviderRestrictions)
        {
            var existingIdentityProviderRestriction = existingClient.IdentityProviderRestrictions.FirstOrDefault(x => x.Provider == identityProviderRestriction.Provider);
            if (existingIdentityProviderRestriction != null)
            {
                identityProviderRestriction.Id = existingIdentityProviderRestriction.Id;
                _clientDataRepository.Detach(existingIdentityProviderRestriction);
            }
        }
    }

    private void SetIdAndDetachExistingPostLogoutRedirectUris(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.PostLogoutRedirectUris == null || clientToUpdate.PostLogoutRedirectUris == null)
        {
            return;
        }

        foreach (var postLogoutRedirectUri in clientToUpdate.PostLogoutRedirectUris)
        {
            var existingPostLogoutRedirectUri = existingClient.PostLogoutRedirectUris.FirstOrDefault(x => x.PostLogoutRedirectUri == postLogoutRedirectUri.PostLogoutRedirectUri);

            if (existingPostLogoutRedirectUri != null)
            {
                postLogoutRedirectUri.Id = existingPostLogoutRedirectUri.Id;
                _clientDataRepository.Detach(existingPostLogoutRedirectUri);
            }
        }
    }

    private void SetIdAndDetachExistingRedirectUris(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.RedirectUris == null || clientToUpdate.RedirectUris == null)
        {
            return;
        }

        foreach (var redirectUri in clientToUpdate.RedirectUris)
        {
            var existingRedirectUri =
                existingClient.RedirectUris.FirstOrDefault(x => x.RedirectUri == redirectUri.RedirectUri);
            if (existingRedirectUri != null)
            {
                redirectUri.Id = existingRedirectUri.Id;
                _clientDataRepository.Detach(existingRedirectUri);
            }
        }
    }

    private void SetIdAndDetachExistingDebugRedirectUris(Client existingClient, Client clientToUpdate)
    {
        if (existingClient.RedirectUris == null)
        {
            return;
        }

        var debugRedirectUris = existingClient.RedirectUris
                                            .Where(x => x.RedirectUri.StartsWith("https://localhost", StringComparison.InvariantCultureIgnoreCase) ||
                                                        x.RedirectUri.StartsWith("http://localhost", StringComparison.InvariantCultureIgnoreCase));

        if (clientToUpdate.RedirectUris == null)
        {
            clientToUpdate.RedirectUris = new List<ClientRedirectUri>();
        }

        foreach (var debugRedirectUri in debugRedirectUris)
        {
            if (clientToUpdate.RedirectUris.Any(t => t.Id == debugRedirectUri.Id))
            {
                continue;
            }

            clientToUpdate.RedirectUris.Add(debugRedirectUri);
            _clientDataRepository.Detach(debugRedirectUri);
        }
    }
}