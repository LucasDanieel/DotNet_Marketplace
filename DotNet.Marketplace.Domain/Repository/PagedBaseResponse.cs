
namespace DotNet.Marketplace.Domain.Repository
{
    public class PagedBaseResponse<T>
    {
        public List<T> Data{ get; set; }
        public int TotalPages { get; set; }
        public int TotalRegister { get; set; }
    }
}
