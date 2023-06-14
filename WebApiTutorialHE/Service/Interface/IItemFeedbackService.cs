using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IItemFeedbackService
    {
        Task<List<ReceivedListModel>> GetByUser();
        Task<ItemFeedback> Updateitemfeedback(ItemfeedbackUpdateModel updateItem);
        Task<string> Deleteitemfeedback(int id);
    }
}
