using Assets.Core.Identity.Service.Dependencies.Extensions;
using Assets.Core.Identity.Service.Configuration;

var builder = WebApplication.CreateBuilder(args);

if (args.Contains("/seed"))
{
    SeedUsers.EnsureSeedData(builder.Configuration);
}
else
{
    LoggerConfig.UseLogger();

    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

    builder.Services.UseIisConfiguration();

    builder.Services.AddHttpContextAccessor();

    // configuration bindings
    builder.Services.AddConfiguration(builder.Configuration);

    builder.Services.RegisterService();

    // identity Server 
    builder.Services.UseIdentityServerDbContext(builder.Configuration);

    builder.Services.UseIdentityServerBuilder(builder.Configuration);

    //builder.Services.AddExternalAuthentication(builder.Configuration); // External OIDC Google; SAML2 IDP

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // identity Server Database init
    app.InitDatabase();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseIdentityServer();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

    app.Run();
}