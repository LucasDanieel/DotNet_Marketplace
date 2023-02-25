
using DotNet.Marketplace.Domain.Validations;

namespace DotNet.Marketplace.Domain.Entity
{
    public class PersonImage
    {
        public int Id { get; private set; }
        public int PersonId { get; private set; }
        public string? ImageUrl { get; private set; }
        public string? ImageUrlCloudinary { get; private set; }
        public string? ImageBase { get; private set; }
        public Person Person { get; private set; }

        public PersonImage(int personId, string? imageUrl, string? imageUrlCloudinary, string? imageBase)
        {
            Validator(personId);
            ImageUrl = imageUrl;
            ImageUrlCloudinary = imageUrlCloudinary;
            ImageBase = imageBase;
        }

        private void Validator(int personId)
        {
            DomainValidator.When(personId < 0, "Id da pessoa deve ser informado");

            PersonId = personId;
        }
    }
}
