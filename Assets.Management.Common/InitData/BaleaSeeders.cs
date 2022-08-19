using Assets.Management.Common.Models;
using Balea;
using Balea.EntityFrameworkCore.Store.DbContexts;
using Balea.EntityFrameworkCore.Store.Entities;

namespace Assets.Management.Common.InitData;

public static class BaleaSeeder
{
    public static async Task Seed(BaleaDbContext db)
    {
       
       if (!db.Roles.Any())
       {
            var aga = new SubjectEntity("Aga", "6b3a3bbc-0f28-4bac-be2b-bd3af11a2d10"); // doctor
            var bob = new SubjectEntity("Bob", "c9fdf14a-a7a9-423a-bf64-0938a33d479c"); // nurse
            var alice = new SubjectEntity("Alice", "bfa7e84c-f3d6-47a1-8ad2-b2f4cc77bbff"); // patient
            var joe = new SubjectEntity("Joe", "455711"); // FC.TidbokAdministrator

            db.Add(aga);
            db.Add(bob);
            db.Add(alice);
            db.Add(joe);

            await db.SaveChangesAsync();
            
            var applicationBuilder = new ApplicationBuilder(BaleaConstants.DefaultApplicationName, "Default application");

            var seePatients = new PermissionEntity(AllPermissions.SeePatients);
            var performSurgery = new PermissionEntity(AllPermissions.PerformSurgery);
            var prescribeMedication = new PermissionEntity(AllPermissions.PrescribeMedication);
            var requestPainMedication = new PermissionEntity(AllPermissions.RequestPainMedication);
            var timeBookUriNamesTbBooking = new PermissionEntity(AllPermissions.TimeBookUriNamesTbBooking);

            applicationBuilder.AddPermission(seePatients)
                              .AddPermission(performSurgery)
                              .AddPermission(prescribeMedication)
                              .AddPermission(timeBookUriNamesTbBooking)
                              .AddPermission(requestPainMedication);


           var doctorRole = new RoleBuilder(Roles.Doctor, "Doctor role")
                                    .AddSubject(aga.Id)
                                    .AddRolePermissionEntity(seePatients)
                                    .AddRolePermissionEntity(performSurgery)
                                    .AddRolePermissionEntity(prescribeMedication)
                                    .RoleEntity;
           
           applicationBuilder.AddRole(doctorRole);

           var nurseRole = new RoleBuilder(Roles.Nurse, "Nurse role")
                                    .AddSubject(bob.Id)
                                    .AddRolePermissionEntity(seePatients)
                                    .AddRolePermissionEntity(prescribeMedication)
                                    .RoleEntity;
        
           applicationBuilder.AddRole(nurseRole);

            var patientRole = new RoleBuilder(Roles.Patient, "Patient role")
                                    .AddSubject(alice.Id)
                                    .AddRolePermissionEntity(requestPainMedication)
                                    .RoleEntity;

            applicationBuilder.AddRole(patientRole);

           var tidbokAdministratorRole = new RoleBuilder(Roles.FcTidbokAdministrator, "FC.TidbokAdministrator role")
                                                .AddSubject(joe.Id)
                                                .AddRolePermissionEntity(timeBookUriNamesTbBooking)
                                                .RoleEntity;

            applicationBuilder.AddRole(tidbokAdministratorRole);

            db.Applications.Add(applicationBuilder.ApplicationEntity);

            await db.SaveChangesAsync();
       }
    }
}