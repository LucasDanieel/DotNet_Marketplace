using DotNet.Marketplace.Domain.Validations;

namespace DotNet.Marketplace.Domain.Entity
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CodErp { get; private set; }
        public decimal Price { get; private set; }
        public ICollection<Purchase> Purchases { get; private set; }

        public Product(string name, string codErp, decimal price)
        {
            Validator(name, codErp, price);
        }

        public Product(int id, string name, string codErp, decimal price)
        {
            DomainValidator.When(id < 0, "Id do produto deve ser informado");
            Id = id;
            Validator(name, codErp, price);
        }

        private void Validator(string name, string codErp, decimal price)
        {
            DomainValidator.When(string.IsNullOrEmpty(name), "Nome deve ser informado");
            DomainValidator.When(string.IsNullOrEmpty(codErp), "Código erp deve ser informado");
            DomainValidator.When(price < 0, "Preço deve ser informado");

            Name = name;
            CodErp = codErp;
            Price = price;
            Purchases = new List<Purchase>();
        }
    }
}
