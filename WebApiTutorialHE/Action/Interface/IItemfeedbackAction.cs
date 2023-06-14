using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IItemfeedbackAction
    {
        Task<ItemFeedback> Updateitemfeedback(ItemfeedbackUpdateModel updateItem);
        Task<string> Deleteitemfeedback(int id);
    }
}
