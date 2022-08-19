using Balea.EntityFrameworkCore.Store.Entities;

namespace Assets.Management.Common.InitData;

    public class RoleBuilder
    {
        public RoleBuilder(string roleName, string roleDescription)
        {
            RoleEntity = new RoleEntity(roleName, roleDescription);
        }

        public RoleBuilder AddSubject(int subjectId)
        {
            RoleEntity.Subjects.Add(new RoleSubjectEntity { SubjectId = subjectId });
            return this;
        }
        
        public RoleBuilder AddRolePermissionEntity(PermissionEntity permission)
        {
            RoleEntity.Permissions.Add(new RolePermissionEntity { Permission = permission });
            return this;
        }

        public RoleEntity RoleEntity { get; }
    }