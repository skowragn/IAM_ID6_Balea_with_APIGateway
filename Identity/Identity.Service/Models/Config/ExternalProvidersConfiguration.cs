using System.Text.Json.Serialization;

namespace Assets.Core.Identity.Service.Models.Config
{
    public class ExternalProvidersConfiguration
    {
        public GoogleProviderConfiguration? GoogleExternalIdentityProvider { get; set; }

        [JsonPropertyName("SAML2P_External_Identity_Provider")]
        public Saml2PProviderConfiguration? SAML2PExternalIdentityProvider { get; set; }
    }
}
