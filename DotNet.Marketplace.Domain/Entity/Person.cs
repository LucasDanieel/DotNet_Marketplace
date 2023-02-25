using DotNet.Marketplace.Domain.Validations;

namespace DotNet.Marketplace.Domain.Entity
{
    public class Person
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Phone { get; private set; }
        public ICollection<Purchase> Purchases { get; private set; }
        public ICollection<PersonImage> PersonImages { get; private set; }

        public Person(string name, string document, string phone)
        {
            Validator(name, document, phone);
        }

        public Person(int id, string name, string document, string phone)
        {
            DomainValidator.When(id < 0, "Id da pessoa deve ser informado");
            Id = id;
            Validator(name, document, phone);
        }

        private void Validator(string name, string document, string phone)
        {
            DomainValidator.When(string.IsNullOrEmpty(name), "Nome deve ser informado");
            DomainValidator.When(string.IsNullOrEmpty(document), "Documento deve ser informado");
            DomainValidator.When(string.IsNullOrEmpty(phone), "Telefone ou celular deve ser informado");

            Name = name;
            Document = document;
            Phone = phone;
            Purchases = new List<Purchase>();
            PersonImages = new List<PersonImage>();
        }
    }
}
