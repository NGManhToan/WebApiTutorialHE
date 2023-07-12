using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IItemFeedbackService
    {
        Task<ObjectResponse> GetByUser(int id);
        Task<ObjectResponse> Updateitemfeedback(ItemfeedbackUpdateModel updateItem);
        Task<string> Deleteitemfeedback(int id);
    }
}
