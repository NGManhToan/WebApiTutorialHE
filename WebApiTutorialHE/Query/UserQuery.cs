using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Query
{
    public class UserQuery:IUserQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public UserQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<UserListModel>> QueryListUser()
        {
            var query = @"SELECT 
                                u.Id,f.Name,u.StudentCode,u.FullName,u.Email,u.PhoneNumber,u.UrlAvatar,u.IsOnline,ur.RoleId
                            FROM
                                User u
                                join Faculty f on u.FacultyId = f.Id	
                                join UserRole ur on u.Id = ur.UserId";
            return await _sharingDapper.QueryAsync<UserListModel>(query);
        }
        public async Task<List<UserRoleModel>> QueryUserRoles()
        {
            var query = @"SELECT r.Id, r.Name, ur.UserId
                           from UserRole ur 
	                        left join Role r on ur.RoleId = r.Id";
            return await _sharingDapper.QueryAsync<UserRoleModel>(query);
        }
        public async Task<UserProfileModel> QueryFrofile(int id)
        {
            var query = @"select u.UrlAvatar,u.FullName,u.StudentCode,f.Name Faculty, u.Class,left(u.Class,3) Session, 
	                        count(p.id) Items, count(r.Id) Shared,u.PhoneNumber,u.Email
                          from User u
	                        left join Post p on u.Id=p.CreatedBy and p.Type = 1
                            left join Registration r on r.PostId=p.Id and r.Status = 2
                            left join Faculty f on f.Id=u.FacultyId
                          where u.Id = @id
                          group by u.Id,u.FullName,u.StudentCode,f.Name, u.Class";
            return await _sharingDapper.QuerySingleAsync<UserProfileModel>(query, new
            {
                Id=id,
            });
        }
        public async Task<List<UserProfileSharingModel>> QueryFrofileSharing(int id)
        {
            var query = @"SELECT 
                                p.id,
                                p.Title,
                                CASE
                                    WHEN p.Price = 0 THEN 'Free'
                                    ELSE p.Price
                                END AS Price,
                                m.ImageUrl
                            FROM
                                Post p
                                    LEFT JOIN
                                User u ON u.Id = p.CreatedBy
                                    LEFT JOIN
                                Registration r ON r.PostId = p.Id AND r.Status != 2
		                            LEFT JOIN
	                            Media m on m.PostId = p.Id
                            WHERE
                                p.Type = 1 AND u.Id = @id and p.IsActive = true
                            GROUP BY p.id , p.Title , p.Price,m.ImageUrl";
            return await _sharingDapper.QueryAsync<UserProfileSharingModel>(query, new
            {
                Id = id,
            });
        }
        public async Task<List<UserProfileFeedback>> QueryItemFeedback(int id)
        {
            var query = @"select u.FullName,i.CreatedDate ,p.Title Item,i.Content
                         from ItemFeedback i
	                        join User u on u.Id=i.CreatedBy
                            join Post p on p.Id=i.PostId
                         where p.CreatedBy=@Id";
            return await _sharingDapper.QueryAsync<UserProfileFeedback>(query, new
            {
                Id=id,
            });
        }
        public async Task<RecipientInformationModel>QueryRecipientInfor(int id)
        {
            var query = @"select FullName, StudentCode, Class, f.Name, PhoneNumber 
                    from User u join Faculty f on u.FacultyId=f.Id
                    where u.Id=@id";
            return await _sharingDapper.QuerySingleAsync<RecipientInformationModel>(query, new
            {
                id = id,
            });
        }
    }
}
