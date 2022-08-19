using Balea.EntityFrameworkCore.Store.DbContexts;
using Microsoft.EntityFrameworkCore;
using Assets.Core.Assets.Management.Api.Dependencies.Extensions;
using Assets.Core.Assets.Management.Api.Extensions;
using Assets.Management.Common.InitData;

var builder = WebApplication.CreateBuilder(args);

    var configuration = builder.Configuration;

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddAuthenticationExt();

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
        });

        var app = builder.Build();
        app.MigrateDbContext<BaleaDbContext>(db => BaleaSeeder.Seed(db).Wait());

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers().RequireAuthorization("ApiScope");

        app.Run();
