using DotNet.Marketplace.Domain.Validations;

namespace DotNet.Marketplace.Domain.Entity
{
    public class UserPermission
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int PermissionId { get; private set; }
        public User User { get; private set; }
        public Permission Permission { get; private set; }

        public UserPermission(int userId, int permissionId)
        {
            Validator(userId, permissionId);
        }

        private void Validator(int userId, int permissionId)
        {
            DomainValidator.When(userId <= 0, "Id do usuario deve ser informado");
            DomainValidator.When(permissionId <= 0, "Id da permissão deve ser informado");

            UserId = userId;
            PermissionId = permissionId;
        }
    }
}
