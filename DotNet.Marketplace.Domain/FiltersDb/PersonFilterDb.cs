
using DotNet.Marketplace.Domain.Repository;

namespace DotNet.Marketplace.Domain.FiltersDb
{
    public class PersonFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; }
    }
}
