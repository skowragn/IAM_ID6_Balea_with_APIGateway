using System.Security.Claims;
using Duende.IdentityServer;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Serilog;
using Assets.Core.Identity.Service.Infrastructure.Context;
using Assets.Core.Identity.Service.Domain.Entities;

namespace Assets.Core.Identity.Service.Configuration;
public class SeedUsers
{
    public static void EnsureSeedData(IConfiguration configuration)
    {
        Log.Information("Seeding database...");

        var connectionString = configuration.GetConnectionString("sqlConnection");
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<IdentityServerDbContext>(options =>
                      options.UseSqlServer(connectionString,
                           opt => opt.MigrationsAssembly(typeof(Program).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityServerDbContext>()
            .AddDefaultTokenProviders();

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        try
        { 
            var context = scope.ServiceProvider.GetService<IdentityServerDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var alice = userMgr.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                    Description = "Super Admin",
                    ValidFrom  = new DateTime(2000, 10, 1),
                    ValidTo = new DateTime(2025,10,1),
                    Disabled = false,
                    HsaId = "12345",
                    NetworkUserId = "12345",
                    Ssn = "123W45",
                    VrkId = "123W45",
                    DomainId = "KonsulPanda"
                };
                var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, alice.UserName),
                            new Claim("Domain", alice.DomainId)
                     
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("alice created");
        }
        else
        {
            Log.Debug("alice already exists");
        }

        var bob = userMgr.FindByNameAsync("bob").Result;
        if (bob == null)
        {
            bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true,
                Description = "Super User",
                ValidFrom = new DateTime(2000,10,1),
                ValidTo = new DateTime(2025,10,1),
                Disabled = false,
                HsaId = "67565",
                NetworkUserId = "67565",
                Ssn = "67565LW",
                VrkId = "67565LW",
                DomainId = "KonsulPanda"
            };
            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, bob.UserName),
                        new Claim("Domain", bob.DomainId)
                    }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("bob created");
        }
        else
        {
            Log.Debug("bob already exists");
        }

        var aga = userMgr.FindByNameAsync("aga").Result;
        if (aga == null)
        {
            aga = new ApplicationUser
            {
                UserName = "aga",
                Email = "Aga@gmail.com",
                EmailConfirmed = true,
                Description = "Super User",
                ValidFrom = new DateTime(2000, 10, 1),
                ValidTo = new DateTime(2025, 10, 1),
                Disabled = false,
                HsaId = "12345",
                NetworkUserId = "12345",
                Ssn = "123W45",
                VrkId = "123W45",
                DomainId = "KonsulPanda"
            };
            var result = userMgr.CreateAsync(aga, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };

            result = userMgr.AddClaimsAsync(aga, new Claim[]{
                    new Claim(JwtClaimTypes.Name, aga.UserName),
                    new Claim(JwtClaimTypes.Role, "doctor"),
                    new Claim(JwtClaimTypes.GivenName, "Aga"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "Aga@gmail.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://aga.com"),
                    new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)

            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("aga created");
        }
        else
        {
            Log.Debug("aga already exists");
        }



        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Log.Information("Done seeding database.");
    }
}