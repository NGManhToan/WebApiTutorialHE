using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Query
{
    public class AUserQuery: IAUserQuey
    {
        private readonly ISharingDapper _sharingDapper;

        public AUserQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }

        public async Task<List<AdminUserListModel>> QueryGetAllUser(ulong userId, OSearchAdminModel oSearch)
        {
            oSearch.CurrentPage = oSearch.CurrentPage < 0 ? 0 : oSearch.CurrentPage;
            oSearch.CurrentDate = string.IsNullOrEmpty(oSearch.CurrentDate)
                ? Utils.DateNow().ToString("yyyy-MM-dd HH:mm:ss:fff")
                : oSearch.CurrentDate;

            oSearch.Limit = oSearch.Limit == 0 ? 25 : oSearch.Limit;
            oSearch.Status = oSearch.Status == 0 ? 0 : oSearch.Status;
            oSearch.RoleId = oSearch.RoleId == 0 ? 0 : oSearch.RoleId;

            var condition = "";

            if (!string.IsNullOrEmpty(oSearch.Name))
            {
                condition += @" and u.name like @Name";
            }

            if (!string.IsNullOrEmpty(oSearch.Email))
            {
                condition += @" and u.email like @Email";
            }

            if (!string.IsNullOrEmpty(oSearch.Phone))
            {
                condition += @" and u.phone like @Phone";
            }

            if (oSearch.RoleId > 0)
            {
                condition += @" and u.role = @RoleId";
            }

            var query = @"SELECT 
                                u.Id,
                                ifnull(u.FacultyId,'')FacultyId,
                                ifnull(u.StudentCode,'')StudentCode,
                                ifnull(u.FullName,N'')FullName,
                                ifnull(u.Email,'')Email,
                                ifnull(u.Password,'')Password,
                                ifnull(u.PhoneNumber,'')PhoneNumber,
                                ifnull(u.Class,'')Class,
                                ifnull(u.UrlAvatar,'')UrlAvatar,
                                (u.CreatedDate)
                            FROM
                                User u
                                limit @offset, @limit";
            return await _sharingDapper.QueryAsync<AdminUserListModel>(query, new
            {
                Id = userId,
                Name = "%" + oSearch.Name.Trim() + "%",
                Email = "%" + oSearch.Email.Trim() + "%",
                Phone = "%" + oSearch.Phone.Trim() + "%",
                RoleId = oSearch.RoleId,
                CurrentDate = oSearch.CurrentDate,
                offset = oSearch.CurrentPage * oSearch.Limit,
                limit = oSearch.Limit
            });
        }
    }
}
