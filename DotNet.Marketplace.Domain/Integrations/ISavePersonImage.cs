
namespace DotNet.Marketplace.Domain.Integrations
{
    public interface ISavePersonImage
    {
        string CreateImageUrl(string imageBase64);
        string CreateImageUrlCloudinary(string imageBase64, string name);
    }
}
