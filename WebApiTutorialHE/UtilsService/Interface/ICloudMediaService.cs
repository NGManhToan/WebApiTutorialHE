using WebApiTutorialHE.Models.CloudMedia;

namespace WebApiTutorialHE.UtilsService.Interface
{
    public interface ICloudMediaService
    {
        Task<List<CloudMediaModel>> SaveFileData(CloudMediaConfig cloudMedia);
        Task<CloudOneMediaModel> SaveOneFileData(CloudOneMediaConfig cloudOneMedia);
    }
}
