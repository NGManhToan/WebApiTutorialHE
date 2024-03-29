﻿using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IUserAction
    {
        //Task<User> AccountUpdateModels(AccountUpdateModel model);

        Task<User> DeleteUser(ForceInfo forceInfo, int id);
        //Task<User> ActionFillterAccount(int id/*, string email*/);

        Task<ActionResult<User>> ChangePassword(UserChangePasswordModel userForgotPassword);
        Task<string>Register(UserRegisterModel userRegisterModel,IFormFile fileName);
        Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata);
        Task<User> UpdateProfile(UserUpdateModel userUpdate, ForceInfo forceInfo);

        //Task UpdatePassword();
        Task<bool> IsEmailDuplicate(string email);
        Task<bool> IsPhoneDuplicate( string phoneNumber);
        Task<string> IdentifyOTP(int userId, string otpCode);
        Task<User> IdentifyOTPUpdate(ForceInfo forceInfo, string otpCode);
        Task ChangePasswordUser(ForceInfo forceInfo, string newPassword);
    }
}
