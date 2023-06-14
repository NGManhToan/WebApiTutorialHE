using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class ItemfeedbackService:IItemFeedbackService
    {
        private readonly IItemfeedbackAction _itemfeedbackAction;
        private readonly IItemfeedbackQuery _itemfeedbackQuery;
        public ItemfeedbackService(IItemfeedbackAction itemfeedbackAction, IItemfeedbackQuery itemfeedbackQuery)
        {
            _itemfeedbackAction = itemfeedbackAction;
            _itemfeedbackQuery = itemfeedbackQuery;
        }

        public async Task<List<ReceivedListModel>> GetByUser()
        {
            return await _itemfeedbackQuery.GetItemfeedback();
        }
        public async Task<ItemFeedback> Updateitemfeedback(ItemfeedbackUpdateModel updateItem)
        {
            return await _itemfeedbackAction.Updateitemfeedback(updateItem);
        }
        public async Task<string> Deleteitemfeedback(int id)
        {
            return await _itemfeedbackAction.Deleteitemfeedback(id);
        }
    }
}
