
using DotNet.Marketplace.Domain.Validations;

namespace DotNet.Marketplace.Domain.Entity
{
    public class Permission
    {
        public int Id { get; private set; }
        public string PermissionName { get; private set; }
        public string PermissionVisual { get; private set; }
        public ICollection<UserPermission> UserPermissions { get; private set; }

        public Permission(string permissionName, string permissionVisual)
        {
            Validator(permissionName, permissionVisual);
        }

        private void Validator(string permissionName, string permissionVisual)
        {
            DomainValidator.When(string.IsNullOrEmpty(permissionName), "Nome da permissão deve ser informado");
            DomainValidator.When(string.IsNullOrEmpty(permissionVisual), "Nome visual da permissão deve ser informado");

            PermissionName = permissionName;
            PermissionVisual = permissionVisual;
            UserPermissions = new List<UserPermission>();
        }
    }
}
