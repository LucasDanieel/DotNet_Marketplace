
namespace DotNet.Marketplace.Domain.Validations
{
    public class DomainValidator : Exception
    {
        public DomainValidator(string error) : base(error)
        { }

        public static void When(bool hasError, string message)
        {
            if (hasError)
                throw new DomainValidator(message);
        }
    }
}
