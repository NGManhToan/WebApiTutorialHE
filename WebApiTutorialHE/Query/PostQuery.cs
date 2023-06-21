using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Models.Registation;

namespace WebApiTutorialHE.Query
{
    public class PostQuery : IPostQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public PostQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<HomePostModel>> QueryHomePost()
        {
            var query =
                @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId WHERE type=1";
            return await _sharingDapper.QueryAsync<HomePostModel>(query);
        }
        public async Task<List<HomePostModel>> QueryFindPost(string search)
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price FROM Post p LEFT JOIN Media m ON p.Id=m.PostId WHERE Title LIKE @search AND type=1";
            var parameters = new { Search = "%" + search + "%" };
            return await _sharingDapper.QueryAsync<HomePostModel>(query, parameters);
        }
        public async Task<List<HomePostModel>> QuerySelectPostFollowCategoryId(int id)
        {
            var query =
                @"SELECT CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price, Title,
                    CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl
                  FROM Post p LEFT JOIN Category c ON p.CategoryId=c.Id
                                LEFT JOIN Media m ON p.Id=m.PostId WHERE CategoryId LIKE @id AND type=1";
            //return await _sharingDapper.QueryAsync<HomePostModel>(query, new
            //{
            //    id = id
            //});
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { id = id });

        }
        public async Task<List<HomeWishModel>> QueryGetWishList()
        {
            var query = @"SELECT u.FullName, p.CreatedDate, Content,CASE 
		                    WHEN DesiredStatus = 3 THEN 'Free, Purchase' 
                            ELSE CAST(DesiredStatus AS char(10))                             
                            END AS DesiredStatus,
                     CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl
                  FROM Post p left JOIN User u ON p.CreatedBy=u.Id
                                left JOIN Media m ON p.Id=m.PostId WHERE type=2";
            return await _sharingDapper.QueryAsync<HomeWishModel>(query);
        }
        public async Task<List<HomePostModel>> QueryAscendPrice(int id)
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id  ORDER BY 
											CASE WHEN Price = 0 THEN 0 ELSE 1 END ASC,Price ASC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { id = id });
        }
        public async Task<List<HomePostModel>> QueryDescendPrice(int id)
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id  ORDER BY 
											CASE WHEN Price = 0 THEN 0 ELSE 1 END DESC,Price DESC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { id = id });
        }
        public async Task<List<HomePostModel>> QueryFilterFreeItem(int id)
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl,
                                Title, 
                                CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                            FROM 
                                Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id
                            AND Price='Free';";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { id = id });
        }
        public async Task<List<DetailItemModel>> QueryGetDetailItem(int postId)
        {
            var query = @"select Title, FullName, p.Status, Content, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price, 
                    CONCAT('" + Utils.LinkMedia("") + @"', m.ImageUrl) as ImageUrl
                    from Post p left join User u on p.CreatedBy=u.Id left join Media m on m.PostId=p.Id
                    where p.Id LIKE @postId and type=1";
            return await _sharingDapper.QueryAsync<DetailItemModel>(query, new { postId = postId });
        }

        public async Task<List<MySharingModel>> QueryGetShareListByUser(int id)
        {
            var query = @"select p.Title,
		                        CONCAT('"" + Utils.LinkMedia("""") + @""', m.ImageUrl) as ImageUrl
                        from Post p
	                         left join Media m on p.Id=m.PostId
	                         join User u on p.CreatedBy=u.Id
                        where p.CreatedBy=5";
            return await _sharingDapper.QueryAsync<MySharingModel>(query, new { id = id });
        }
        public async Task<List<DetailWishListModel>> QueryDetailWishList(int wishId)
        {
            var query = @"select CONCAT('"" + Utils.LinkMedia("""") + @""',u.UrlAvatar) as UrlAvatar, 
	                    FullName, p.CreatedDate, Content, c.Name, 
                        CONCAT('"" + Utils.LinkMedia("""") + @""', m.ImageUrl) as ImageUrl, 
                        CASE 
		                    WHEN DesiredStatus = 3 THEN 'Free, Purchase' 
                            ELSE CAST(DesiredStatus AS char(10))                             
                            END AS DesiredStatus
                    from User u left join Post p on u.Id=p.CreatedBy
			                    left join Category c on c.Id=p.CategoryId
                                left join Media m on m.PostId=p.Id
                    where p.Id = @wishId and type=2;";
            return await _sharingDapper.QueryAsync<DetailWishListModel>(query, new { wishId = wishId });
        }

        public async Task<List<CommentModel>> GetListComment(int postId)
        {
            var query = @"SELECT c.Id,u.UrlAvatar, u.FullName, c.Content,
	                        case  
                            when timediff(NOW(), c.CreatedDate)> '24:00:00' then concat(datediff(@Now, c.CreatedDate), ' ngày trước')
                            when timediff(NOW(), c.CreatedDate)> '1:00:00' then concat(hour(timediff(@Now, c.CreatedDate)), ' giờ trước')
                            else concat(minute(timediff(NOW(), c.CreatedDate)), ' phút trước')
                            end TimeDiff
                        FROM Comment c
	                        JOIN User u ON u.Id = c.CreatedBy
                            JOIN Post p ON p.Id = c.PostId
                        WHERE p.Id=@PostId";
            return await _sharingDapper.QueryAsync<CommentModel>(query, new
            {
                PostId = postId,
                Now = Utils.DateNow()
            });
        }
        
    }
}
