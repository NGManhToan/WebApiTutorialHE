using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.UtilsService.Interface;


namespace WebApiTutorialHE.UtilsService
{
    [Obsolete]
    public class CloudMediaService : ICloudMediaService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly string _webRoot;

        public CloudMediaService(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _env = env;
            _webRoot = _env.WebRootPath;
        }

        public async Task<List<CloudMediaModel>> SaveFileData(CloudMediaConfig cloudMedia)
        {
            var cloudMedias = new List<CloudMediaModel>();

            if (cloudMedia.CloudFiles != null && cloudMedia.CloudFiles.Count > 0)
            {
                foreach (var file in cloudMedia.CloudFiles)
                {
                    if (file.FormFile != null && file.FormFile.Length > 0)
                    {
                        var serverPath = Path.Combine(_webRoot, cloudMedia.Folder);

                        bool exists = Directory.Exists(serverPath);
                        if (!exists)
                        {
                            Directory.CreateDirectory(serverPath);
                        }

                        string extension = Path.GetExtension(file.FormFile.FileName);
                        string nameImage = cloudMedia.FileName + "_" + Utils.GenerateID() + extension;

                        var path = Path.Combine(serverPath, nameImage);
                        using var fileStream = new FileStream(path, FileMode.Create);
                        await file.FormFile.CopyToAsync(fileStream);

                        cloudMedias.Add(new CloudMediaModel
                        {
                            Index = file.Index,
                            FileName = nameImage,
                        });
                    }
                }
            }

            return cloudMedias;
        }

        public async Task<CloudOneMediaModel> SaveOneFileData(CloudOneMediaConfig cloudOneMedia)
        {
            var cloudOneMediaModel = new CloudOneMediaModel();
            
            var root = Directory.GetCurrentDirectory();

            if (cloudOneMedia.FormFile != null && cloudOneMedia.FormFile.Length > 0)
            {
                var serverPath = Path.Combine(root, cloudOneMedia.Folder);

                bool exists = Directory.Exists(serverPath);
                if (!exists)
                {
                    Directory.CreateDirectory(serverPath);
                }

                string extension = Path.GetExtension(cloudOneMedia.FormFile.FileName);
                string nameImage = cloudOneMedia.FileName + "_" + Utils.GenerateID() + extension;

                var path = Path.Combine(serverPath, nameImage);
                using var fileStream = new FileStream(path, FileMode.Create);
                await cloudOneMedia.FormFile.CopyToAsync(fileStream);

                cloudOneMediaModel = new CloudOneMediaModel
                {
                    FileName = nameImage,
                };
            }

            return cloudOneMediaModel;
        }
    }
}
