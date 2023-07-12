using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;

namespace WebApiTutorialHE.Action
{
    public class ItemfeedbackAction:IItemfeedbackAction
    {
        private readonly SharingContext _sharingContext;
        public ItemfeedbackAction(SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }
        public async Task<ItemFeedback> Updateitemfeedback(ItemfeedbackUpdateModel updateItem)
        {
            var update= await _sharingContext.ItemFeedbacks.FindAsync(updateItem.Id);
            if (update != null)
            {
                update.Content = updateItem.Content;
                _sharingContext.ItemFeedbacks.Update(update);
                await _sharingContext.SaveChangesAsync();
            }
            return update;
        }

        public async Task<string> Deleteitemfeedback(int id)
        {
            var deletefeebback = await _sharingContext.ItemFeedbacks.FindAsync(id);
            deletefeebback.IsDeleted=true;
            _sharingContext.ItemFeedbacks.Update(deletefeebback);
            await _sharingContext.SaveChangesAsync();
            return "Đã xóa";
           
        }
    }
}
