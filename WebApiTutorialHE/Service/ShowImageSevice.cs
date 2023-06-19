using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class ShowImageSevice:IShowImageSevice
    {
        public async Task<byte[]> ShowImageAsync(string fileName)
        {
            var path = Path.Combine("wwwroot", "Upload", "Avata", fileName);
            var fullPath = Directory.GetCurrentDirectory();

            if (File.Exists(path))
            {
                return await File.ReadAllBytesAsync(path);
            }

            return null;
        }
    }
}
