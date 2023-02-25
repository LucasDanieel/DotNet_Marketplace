using DotNet.Marketplace.Domain.Validations;
using System.Diagnostics;
using System.Xml.Linq;

namespace DotNet.Marketplace.Domain.Entity
{
    public class Purchase
    {
        public int Id { get; private set; }
        public int PersonId { get; private set; }
        public int ProductId { get; private set; }
        public DateTime DateTime { get; private set; }
        public Person Person { get; private set; }
        public Product Product { get; private set; }

        public Purchase(int personId, int productId)
        {
            Validator(personId, productId);
        }

        public Purchase(int id, int personId, int productId)
        {
            DomainValidator.When(id < 0, "Id da compra deve ser informado");
            Id = id;
            Validator(personId, productId);
        }

        public void Edit(int id, int personId, int productId)
        {
            DomainValidator.When(id <= 0, "Id da compra deve ser informado");
            Id = id;
            Validator(personId, productId);
        }

        private void Validator(int personId, int productId)
        {
            DomainValidator.When(personId <= 0, "Id da pessoa deve ser informado");
            DomainValidator.When(productId <= 0, "Id do produto deve ser informado");

            PersonId = personId;
            ProductId = productId;
            DateTime = DateTime.Now;
        }
    }
}
