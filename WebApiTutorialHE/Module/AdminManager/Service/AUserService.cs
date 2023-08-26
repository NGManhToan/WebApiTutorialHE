using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Models.Admin;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Service
{
    public class AUserService: IAUserService
    {
        private readonly IAUserQuey _userQuey;
        private readonly IAUserAction _userAction;
        public AUserService (IAUserQuey userQuey,IAUserAction userAction)
        {
            _userQuey = userQuey;
            _userAction = userAction;
        }

        public async Task<List<AdminUserListModel>> GetListAccountUser(ulong userId, OSearchAdminModel oSearch)
        {
            var getAccount = await _userQuey.QueryGetAllUser(userId, oSearch);
            return getAccount;
        }

        public async Task<ObjectResponse> AddUserByAdmin(AdminCreateUserModel adminCreate, ForceInfo forceInfo)
        {

            var addAdmin = await _userAction.AddUser(adminCreate, forceInfo);
            if (addAdmin == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "User không có quyền tạo admin !"
                };
            }
            return new ObjectResponse
            {
                result = 1,
                message = "Tạo thành công",
                content = addAdmin
            };

        }

        public async Task<ObjectResponse> AddAdminByAdmin(AdminCreateManagerModel adminCreate, ForceInfo forceInfo)
        {
            
            var addAdmin = await _userAction.AddManage(adminCreate, forceInfo);
            if(addAdmin == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "User không có quyền tạo admin !"
                };
            }
            return new ObjectResponse
            {
                result = 1,
                message = "Tạo thành công",
                content = addAdmin
            };
            
        }

        public async Task<ObjectResponse> AdminUpdateProfile(AdminUpdateManagerModel adminUpdate, ForceInfo forceInfo)
        {
            var updateProfile = await _userAction.AUpdateProfile(adminUpdate, forceInfo);
            if (updateProfile == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Xác thực không thành công !!"
                };
            }
            return new ObjectResponse
            {
                result = 1,
                message = "Cập nhật hồ sơ thành công."
            };
        }
    }
}
