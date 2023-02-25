namespace DotNet.Marketplace.Application.DTOs
{
    public class PagedBaseResponseDTO<T>
    {
        public List<T> Data { get; set; }
        public int TotalRegisters { get; set; }

        public PagedBaseResponseDTO(List<T> data, int totalRegisters)
        {
            Data = data;
            TotalRegisters = totalRegisters;
        }

    }
}
