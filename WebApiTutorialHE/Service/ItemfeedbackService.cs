using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Models.UtilsProject;
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

        public async Task<ObjectResponse> GetByUser(int id)
        {
            var getByUser= await _itemfeedbackQuery.GetItemfeedback(id);
            return new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công",
                content=getByUser
            };
        }
        public async Task<ObjectResponse> Updateitemfeedback(ItemfeedbackUpdateModel updateItem)
        {
            var updateFeedback= await _itemfeedbackAction.Updateitemfeedback(updateItem);
            return new ObjectResponse
            {
                result = 1,
                message = "Đã cập nhật",
                content = updateFeedback
            };
        }
        public async Task<ObjectResponse> Deleteitemfeedback(int id)
        {
            return new ObjectResponse
            {
                result = 1,
                message = "Đã xóa",
            };
        }
    }
}
