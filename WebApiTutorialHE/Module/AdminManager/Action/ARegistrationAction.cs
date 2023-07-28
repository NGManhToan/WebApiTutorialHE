using Microsoft.EntityFrameworkCore;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.Registration;

namespace WebApiTutorialHE.Module.AdminManager.Action
{
    public class ARegistrationAction:IARegistrationAction
    {
        private readonly SharingContext _sharingContext;
        
        public ARegistrationAction(SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }

        public async Task<List<string>> UpdateStatus(AUpdateStatusModel aUpdateStatus)
        {
            var updateStatus = await _sharingContext.Registrations.FindAsync(aUpdateStatus.Id);
            if (updateStatus == null)
            {
                throw new BadHttpRequestException("Id không phù hợp!");
            }

            // Kiểm tra xem trạng thái mới từ frontend có hợp lệ hay không
            if (Enum.IsDefined(typeof(StatusEnumModel), aUpdateStatus.Status))
            {
                updateStatus.Status = aUpdateStatus.Status.ToString();
            }
            else
            {
                throw new ArgumentException("Trạng thái không hợp lệ!");
            }

            _sharingContext.Registrations.Update(updateStatus);
            // Lưu thay đổi vào cơ sở dữ liệu
            await _sharingContext.SaveChangesAsync();

            // Lấy danh sách các trạng thái và trả về
            var statusList = Enum.GetNames(typeof(StatusEnumModel)).ToList();
            return statusList;
        }



    }
}
