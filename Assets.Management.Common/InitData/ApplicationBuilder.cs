using Balea.EntityFrameworkCore.Store.Entities;

namespace Assets.Management.Common.InitData;
    public class ApplicationBuilder
    {
        public ApplicationBuilder(string name, string description)
        {
            ApplicationEntity = new ApplicationEntity(name, description);
        }

        public ApplicationBuilder AddPermission(PermissionEntity permission)
        {
            ApplicationEntity.Permissions.Add(permission);
            return this;
        }

        public ApplicationBuilder AddRole(RoleEntity role)
        {
            ApplicationEntity.Roles.Add(role);
            return this;
        }


        public ApplicationEntity ApplicationEntity { get; }
    }