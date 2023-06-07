using WebApiTutorialHE.Action;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class UserService:IUserService
    {
        private readonly IUserAction _userAction;
        public UserService(IUserAction userAction)
        {
            _userAction = userAction;
        }
        public async Task<ObjectResponse> ChangePassword(UserChangePasswordModel userForgotPassword)
        {
            if (string.IsNullOrEmpty(userForgotPassword.Email.Trim()))
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Vui lòng nhập email"
                };
            }

            if (!userForgotPassword.NewPassword.Trim().Equals(userForgotPassword.RepeatPassword.Trim()))
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Password và RepeatPassword không trùng khớp. Vui lòng thử lại."
                };
            }

            var fpwd = await _userAction.ChangePassword(userForgotPassword);

            if (fpwd == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Thay đổi password thất bại. Vui lòng thử lại."
                };
            }

            return new ObjectResponse
            {
                result = 1,
                message = "Thay đổi password thành công."
            };
        }

        public async Task<Tuple<Account,User>> Register(UserRegisterModel userRegisterModel)
        {
            await _userAction.SaveOneMediaData(userRegisterModel.url_avatar);

            var register = await _userAction.Register(userRegisterModel);
            
            return register;
        }
    }
}
