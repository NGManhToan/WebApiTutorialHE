using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Models.Notification;
using WebApiTutorialHE.Service;

namespace WebApiTutorialHE.Action
{
    public class ItemfeedbackAction:IItemfeedbackAction
    {
        private readonly SharingContext _sharingContext;
        private readonly SharingHub _sharingHub;
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

		public async Task<ItemFeedback> Deleteitemfeedback(int id, string connectionId)
		{
			var deletefeebback = await _sharingContext.ItemFeedbacks.FindAsync(id);
			deletefeebback.IsDeleted = true;
			_sharingContext.ItemFeedbacks.Update(deletefeebback);
			await _sharingContext.SaveChangesAsync();

			// Tạo thông báo
			ENotificationListModel notification = new ENotificationListModel { Content = "Đã xóa thành công" };

			// Tạo danh sách userId
			List<int> toUserIds = new List<int> { /* danh sách các userId cần gửi thông báo */ };

			// Gửi thông báo đến người dùng
			await _sharingHub.EducationSendMessOnUser(toUserIds, notification);

			return deletefeebback;
		}


	}
}
