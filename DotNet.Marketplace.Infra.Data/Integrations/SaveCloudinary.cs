using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace DotNet.Marketplace.Infra.Data.Integrations
{
    public class SaveCloudinary
    {
        private readonly Cloudinary _cloudinary;
        private readonly string CLOUD_NAME = "CLOUD_NAME";
        private readonly string API_KEY = "API_KEY";
        private readonly string API_SECRET = "API_SECRET";

        public SaveCloudinary()
        {
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            _cloudinary = new Cloudinary(account);
        }

        public string SaveImageCloudinary(string imagePath, string name)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath),
                PublicId = name,
            };

            var uploadResult = _cloudinary.Upload(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }
    }
}
