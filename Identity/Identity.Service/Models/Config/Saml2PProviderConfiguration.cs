namespace Assets.Core.Identity.Service.Models.Config;
public class Saml2PProviderConfiguration
{
    public string? ProviderName { get; set; }
    public string? Licensee { get; set; }
    public string? LicenseKey { get; set; }
    public string? NameIdClaimType { get; set; }
    public string? CallbackPath { get; set; }
    public string? SignInScheme { get; set; }
    public string? IdentityProviderOptionsEntityId { get; set; }
    public string? IdentityProviderOptionsSsoEndpoint { get; set; }
    public string? IdentityProviderOptionsSloEndpoint { get; set; }
    public string? ServiceProviderOptionsEntityId { get; set; }
    public string? ServiceProviderOptionsMetadataPath { get; set; }
}