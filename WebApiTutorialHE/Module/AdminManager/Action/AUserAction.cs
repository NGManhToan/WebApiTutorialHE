using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Models.Admin;

namespace WebApiTutorialHE.Module.AdminManager.Action
{
    public class AUserAction:IAUserAction
    {
        private readonly SharingContext _sharingContext;

        public AUserAction(SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }
        public async Task<User> AddUser(AdminCreateUserModel adminCreate,ForceInfo forceInfo)
        {
            try
            {
                var userRole = await _sharingContext.UserRoles
                 .Where(ur => ur.UserId == forceInfo.UserId)
                 .Select(ur => ur.Role)
                 .FirstOrDefaultAsync();

                if (userRole.Id != null)
                {
                    if(userRole.Id == 1 || userRole.Id ==2)
                    {
                        var repeat = Encryptor.SHA256Encode(adminCreate.RepeatPassword.Trim());
                        var create = new User()
                        {
                            Email = adminCreate.Email,
                            Password = Encryptor.SHA256Encode(adminCreate.Password.Trim()),
                            PhoneNumber = adminCreate.Phone,
                            FullName = adminCreate.Name,
                            FacultyId = adminCreate.Faculty,
                            CreatedBy = forceInfo.UserId,
                            LastModifiedBy = forceInfo.UserId,
                            CreatedDate = forceInfo.DateNow,
                            Class = adminCreate.Class,
                            StudentCode = adminCreate.StudentCode,
                        };

                        if (create.Password.CompareTo(repeat) != 0)
                        {
                            throw new BadHttpRequestException($"Mật khẩu và Mật khẩu xác nhận không trùng khớp.");

                        }

                        if (adminCreate.Avatar != null)
                        {
                            var uploader = new Uploadfirebase();
                            byte[] imageData;
                            using (var memoryStream = new MemoryStream())
                            {
                                await adminCreate.Avatar.CopyToAsync(memoryStream);
                                imageData = memoryStream.ToArray();
                            }
                            string imageUrl = await uploader.UploadAvatar(imageData, adminCreate.Avatar.FileName);
                            // Lưu link của ảnh vào thuộc tính UrlAvatar của user
                            create.UrlAvatar = imageUrl;

                        }
                        else
                        {
                            create.UrlAvatar = null;
                        }

                        _sharingContext.Users.Add(create);
                        await _sharingContext.SaveChangesAsync();

                        var role = await _sharingContext.Roles.FindAsync(3);
                        create.UserRoles.Add(new UserRole { UserId = create.Id, RoleId = role.Id });

                        //_sharingContext.UserRoles.Add(role);
                        await _sharingContext.SaveChangesAsync();

                        create.IsActive = true;


                        await _sharingContext.SaveChangesAsync();

                        return create;
                    }
                    else
                    {
                            throw new BadHttpRequestException($"User không có quyền tạo admin !");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        
        public async Task<User> AddManage(AdminCreateManagerModel adminCreate, ForceInfo forceInfo)
        {
            try
            {
                var userRole = await _sharingContext.UserRoles
                 .Where(ur => ur.UserId == forceInfo.UserId)
                 .Select(ur => ur.Role)
                 .FirstOrDefaultAsync();

                if (userRole.Id != null)
                {
                    if (userRole.Id == 1)
                    {
                        var repeat = Encryptor.SHA256Encode(adminCreate.RepeatPassword.Trim());
                        var create = new User()
                        {
                            Email = adminCreate.Email,
                            Password = Encryptor.SHA256Encode(adminCreate.Password.Trim()),
                            PhoneNumber = adminCreate.Phone,
                            FullName = adminCreate.Name,
                            FacultyId = 1,
                            CreatedBy = forceInfo.UserId,
                            LastModifiedBy = forceInfo.UserId,
                            CreatedDate = forceInfo.DateNow,
                            Class = adminCreate.Class,
                            StudentCode = adminCreate.StudentCode,
                        };

                        if (create.Password.CompareTo(repeat) != 0)
                        {
                            Console.WriteLine($"Mật khẩu và Mật khẩu xác nhận không trùng khớp.");

                        }

                        if (adminCreate.Avatar != null)
                        {
                            var uploader = new Uploadfirebase();
                            byte[] imageData;
                            using (var memoryStream = new MemoryStream())
                            {
                                await adminCreate.Avatar.CopyToAsync(memoryStream);
                                imageData = memoryStream.ToArray();
                            }
                            string imageUrl = await uploader.UploadAvatar(imageData, adminCreate.Avatar.FileName);
                            // Lưu link của ảnh vào thuộc tính UrlAvatar của user
                            create.UrlAvatar = imageUrl;

                        }
                        else
                        {
                            create.UrlAvatar = null;
                        }

                        _sharingContext.Users.Add(create);
                        await _sharingContext.SaveChangesAsync();

                        var role = await _sharingContext.Roles.FindAsync(2);
                        create.UserRoles.Add(new UserRole { UserId = create.Id, RoleId = role.Id });

                        //_sharingContext.UserRoles.Add(role);
                        await _sharingContext.SaveChangesAsync();

                        create.IsActive = true;


                        await _sharingContext.SaveChangesAsync();

                        return create;
                    }
                    else
                    {
                        Console.WriteLine($"User không có quyền tạo admin !");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<User> AUpdateProfile(AdminUpdateManagerModel adminUpdate,ForceInfo forceInfo)
        {
            var update = await _sharingContext.Users.FindAsync(adminUpdate.Id);

            if (adminUpdate.Id == 0 || adminUpdate.Id == ' ')
            {
                return null;
            }

            if (update != null)
            {
                if (!string.IsNullOrEmpty(adminUpdate.FullName))
                {
                    update.FullName = adminUpdate.FullName;
                }

                if (!string.IsNullOrEmpty(adminUpdate.Email))
                {
                    update.Email = adminUpdate.Email;
                }

                if (!string.IsNullOrEmpty(adminUpdate.Phone))
                {
                    update.PhoneNumber = adminUpdate.Phone;
                }

                if (!string.IsNullOrEmpty(adminUpdate.StudentCode))
                {
                    update.StudentCode = adminUpdate.StudentCode;
                }

                if (adminUpdate.FacultyId != null && adminUpdate.FacultyId != 0)
                {
                    update.FacultyId = adminUpdate.FacultyId.Value; // Use Value property to get the non-nullable value
                }

                if (!string.IsNullOrEmpty(adminUpdate.Class))
                {
                    update.Class = adminUpdate.Class;
                }
                // Cập nhật các trường không thay đổi

                update.LastModifiedDate = Utils.DateNow();
                update.LastModifiedBy = forceInfo.UserId;

                if (adminUpdate.FileName != null)
                {
                    var uploader = new Uploadfirebase();
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        await adminUpdate.FileName.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                    string imageUrl = await uploader.UploadAvatar(imageData, adminUpdate.FileName.FileName);
                    // Lưu link của ảnh vào thuộc tính UrlAvatar của user
                    update.UrlAvatar = imageUrl;

                }
                else
                {
                    update.UrlAvatar = null;
                }

                _sharingContext.Users.Update(update);
                await _sharingContext.SaveChangesAsync();
            }

            return update;
        }
    }
}
