
using DotNet.Marketplace.Domain.Integrations;
using System.Text;

namespace DotNet.Marketplace.Infra.Data.Integrations
{
    public class SavePersonImage : ISavePersonImage
    {
        private readonly string _path;

        public SavePersonImage()
        {
            _path = "C:/temp/aMarketplace";
        }
        public string CreateImageUrl(string imageBase64)
        {
            var fileExt = imageBase64.Substring(
                imageBase64.IndexOf("/") + 1, imageBase64.IndexOf(";") - imageBase64.IndexOf("/") - 1);

            var imageBase = imageBase64.Substring(imageBase64.IndexOf(",") + 1);
            var imageByte = Convert.FromBase64String(imageBase);

            var imageName = Guid.NewGuid().ToString() + "." + fileExt;
            using (var image = new FileStream(_path + "/" + imageName, FileMode.Create))
            {
                image.Write(imageByte, 0, imageByte.Length);
                image.Flush();
            };

            return _path + "/" + imageName;
        }

        public string CreateImageUrlCloudinary(string imageBase64, string name)
        {
            var fileExt = imageBase64.Substring(
                imageBase64.IndexOf("/") + 1, imageBase64.IndexOf(";") - imageBase64.IndexOf("/") - 1);

            var imageBase = imageBase64.Substring(imageBase64.IndexOf(",") + 1);
            var imageByte = Convert.FromBase64String(imageBase);

            var imageName = $"{name}-{Guid.NewGuid()}";;
            var imagePath = _path + "/" + imageName + "." + fileExt;

            using (var image = new FileStream(imagePath, FileMode.Create))
            {
                image.Write(imageByte, 0, imageByte.Length);
                image.Flush();
            };

            var saveCloudinary = new SaveCloudinary();
            var imageCloudinary = saveCloudinary.SaveImageCloudinary(imagePath, imageName);

            File.Delete(imagePath);

            return imageCloudinary;
        }
    }
}
