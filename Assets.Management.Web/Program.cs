using Assets.Management.Common.InitData;
using Balea.EntityFrameworkCore.Store.DbContexts;
using Microsoft.EntityFrameworkCore;
using Assets.Management.Common.Interfaces;
using Assets.Management.Infrastructure.Repositories;
using Assets.Management.Web.Dependency.Injection;
using Assets.Management.Web.Extensions;


var builder = WebApplication.CreateBuilder(args);

    var configuration = builder.Configuration;
  
    builder.Services
        .AddBalea()
        .AddEntityFrameworkCoreStore(options =>
        {
            options.ConfigureDbContext = builder =>
            {
                var connectionString = configuration.GetConnectionString("sqlConnection");
                
                builder.UseSqlServer(connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                    });
            };
        })
        .Services
        .AddAuthenticationExt()
        .AddScoped<IPatientsServiceRepository,PatientsServiceRepository>()
        .AddControllersWithViews();

    var app = builder.Build();
    app.MigrateDbContext<BaleaDbContext>(db => BaleaSeeder.Seed(db).Wait());

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();