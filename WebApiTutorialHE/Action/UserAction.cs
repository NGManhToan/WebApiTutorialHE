﻿using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiTutorialHE.Database.SharingModels;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.UtilsProject;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.UtilsService.Interface;
using System.Linq;
using Google.Cloud.Storage.V1;
using System.Security.AccessControl;

namespace WebApiTutorialHE.Action
{
    public class UserAction:IUserAction
    {
        private readonly SharingContext _sharingContext;
        private readonly ICloudMediaService _cloudMediaService;

        public UserAction(SharingContext sharingContext,ICloudMediaService cloudMediaService)
        {
            _sharingContext = sharingContext;
            _cloudMediaService = cloudMediaService;
        }



        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var deleteAccount = await _sharingContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (deleteAccount == null)
            {
                return new NotFoundResult();
            }

            deleteAccount.IsDeleted = true;
            await _sharingContext.SaveChangesAsync();

            return "Đã xóa";
        }



        public async Task<ActionResult<User>> ChangePassword(UserChangePasswordModel userForgotPassword)
        {
            var user = await _sharingContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(userForgotPassword.Email));
            if (user != null)
            {
                user.Password = Encryptor.SHA256Encode(userForgotPassword.NewPassword.Trim());
                _sharingContext.Update(user);
                await _sharingContext.SaveChangesAsync();
            }

            return user;
        }


        public async Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata)
        {
            var cloudOneMediaConfig = new CloudOneMediaConfig
            {
                Folder = Path.Combine("wwwroot", "Upload","Avata"),
                FileName = "Image_Avatar",
                FormFile = avata,
            };
            return await _cloudMediaService.SaveOneFileData(cloudOneMediaConfig);
        }
        public async Task<bool> IsEmailDuplicate(string email)
        {
            return await _sharingContext.Users.AnyAsync(u => u.Email.Equals(email) );
        }

        public async Task<bool> IsPhoneDuplicate(string phoneNumber)
        {
            return await _sharingContext.Users.AnyAsync(u => u.PhoneNumber.Equals(phoneNumber));
        }

        public async Task<ActionResult<UserReturnRegister>> Register(UserRegisterModel userRegisterModel, string fileName)
        {
            var storageClient = await StorageClient.CreateAsync();
            var objectName = $"Upload/Avata/{fileName}"; // Path to the file in Firebase Storage
            var bucketName = "gs://sharingtogether-c8be8.appspot.com/";

            using (var fileStream = System.IO.File.OpenRead(fileName))
            {
                // Upload the file to Firebase Cloud Storage
                await storageClient.UploadObjectAsync(bucketName, objectName, null, fileStream);
            }

            var user = new User
            {
                Email = userRegisterModel.Email,
                Password = Encryptor.SHA256Encode(userRegisterModel.Password.Trim()),
                FullName = userRegisterModel.FullName ?? string.Empty,
                PhoneNumber = userRegisterModel.PhoneNumber,
                Class = userRegisterModel.Class,
                StudentCode = userRegisterModel.StudentCode,
                FacultyId = userRegisterModel.FacultyId,
                UrlAvatar = fileName,
            };

            var role = await _sharingContext.Roles.FindAsync(3);
            user.Roles.Add(role);

            _sharingContext.Users.Add(user);
            await _sharingContext.SaveChangesAsync();

            return new UserReturnRegister
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Class = user.Class,
                StudentCode = user.StudentCode,
                FacultyId = user.FacultyId,
                UrlAvatar = $"https://storage.googleapis.com/{bucketName}/{objectName}",
                RoleIDs = user.Roles.Select(x => x.Id).ToList()
            };
        }

        public async Task<User> UpdateProfile(UserUpdateModel userUpdate, string filename)
        {
            var update = await _sharingContext.Users.FindAsync(userUpdate.Id);

            if (update != null)
            {
                update.FullName = userUpdate.FullName;
                update.Class = userUpdate.Class;
                update.StudentCode = userUpdate.StudentCode;
                update.FacultyId = userUpdate.FacultyId;
                update.LastModifiedDate = Utils.DateNow();
                update.LastModifiedBy = update.Id;

                if (userUpdate.UrlImage != null)
                {
                    update.UrlAvatar = filename;
                }

                _sharingContext.Users.Update(update);
                await _sharingContext.SaveChangesAsync();
            };
            return update;
        }
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("user_id", typeof(int));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("password", typeof(string));
            var _list = this._sharingContext.Users.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.Email, item.Password);
                });
            }
            return dt;
        }
    }
}
