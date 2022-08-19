namespace Assets.Core.Identity.Service.Models.Config;

public class GoogleProviderConfiguration
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? CallbackPath { get; set; }
}
