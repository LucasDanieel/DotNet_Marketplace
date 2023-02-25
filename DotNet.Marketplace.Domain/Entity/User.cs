using DotNet.Marketplace.Domain.Validations;

namespace DotNet.Marketplace.Domain.Entity
{
    public class User
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ICollection<UserPermission> UserPermissions { get; private set; }

        public User(string email, string password)
        {
            Validator(email, password);
        }

        public User(int id, string email, string password)
        {
            DomainValidator.When(id < 0, "Id do usuario deve ser informado");
            Id = id;
            Validator(email, password);
        }

        private void Validator(string email, string password)
        {
            DomainValidator.When(string.IsNullOrEmpty(email), "Email deve ser informado");
            DomainValidator.When(string.IsNullOrEmpty(password), "Senha deve ser informada");

            Email = email;
            Password = password;
            UserPermissions = new List<UserPermission>();
        }
    }
}
