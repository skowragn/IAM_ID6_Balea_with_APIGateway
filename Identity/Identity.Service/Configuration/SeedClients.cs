using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Assets.Core.Identity.Service.Configuration;
    public static class SeedClients
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(), 
                new IdentityResource("roles", new[] { "role" })
            };
     
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    // Welfare.Core.Assets.Management.Api
                    // machine to machine - client credentials
                    ClientId = "client",
                    ClientName = "API Identity Server Client",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1", "roles" }
                },
                new()
                {
                    // Welfare.Core.Assets.Management.Web
                    // interactive application (authorize) - ASP.NET Core 6 MVC - code + PKCE
                    ClientId = "interactive",
                    ClientName = "interactive",

                    ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},
                    
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:7024/signin-oidc" },
                    PostLogoutRedirectUris = {"https://localhost:7024/signout-callback-oidc"},

                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "api1"

                    },
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
    }