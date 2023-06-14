using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Query
{
    public class PostQuery:IPostQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public PostQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<HomePostModel>> QueryHomePost()
        {
            var query =
                @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl,
                    Title, Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId WHERE type=1";
            return await _sharingDapper.QueryAsync<HomePostModel>(query);
        }
        public async Task<List<HomePostModel>> QueryFindPost(string search)
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl,
                    Title, Price FROM Post p LEFT JOIN Media m ON p.Id=m.PostId WHERE Title LIKE @search AND type=1";
            var parameters = new { Search = "%" + search + "%" };
            return await _sharingDapper.QueryAsync<HomePostModel>(query, parameters);
        }
        public async Task<List<HomePostModel>> QuerySelectPostFollowCategoryId(int id)
        {
            var query =
                @"SELECT Price, Title,
                    CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl
                  FROM Post p LEFT JOIN Category c ON p.CategoryId=c.Id
                                LEFT JOIN Media m ON p.Id=m.PostId WHERE CategoryId LIKE @id AND type=1";
            //return await _sharingDapper.QueryAsync<HomePostModel>(query, new
            //{
            //    id = id
            //});
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new {id=id});

        }
        public async Task<List<HomeWishModel>> QueryGetWishList()
        {
            var query= @"SELECT u.FullName, p.CreatedDate, Content,DesiredStatus,
                     CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl
                  FROM Post p left JOIN User u ON p.CreatedBy=u.Id
                                left JOIN Media m ON p.Id=m.PostId WHERE type=2";
            return await _sharingDapper.QueryAsync<HomeWishModel>(query);
        }
        public async Task<List<HomePostModel>> QueryAscendPrice(int id)
        {
            var query= @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id  ORDER BY 
											CASE WHEN Price = 0 THEN 0 ELSE 1 END ASC,Price ASC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new {id=id});
        }
        public async Task<List<HomePostModel>> QueryDescendPrice(int id)
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id  ORDER BY 
											CASE WHEN Price = 0 THEN 0 ELSE 1 END DESC,Price DESC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new {id=id});
        }
        public async Task<List<HomePostModel>> QueryFilterFreeItem(int id)
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl,
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
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new {id=id});
        }
        public async Task<List<HomePostModel>>QueryGetDetailItem(int postId)
        {
            var query = @"select Title, FullName, p.Status, Content, DesiredStatus, 
                    CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl
                    from Post p left join User u on p.CreatedBy=u.Id left join Media m on m.PostId=p.Id
                    where p.Id LIKE @postId and type=1";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { postId=postId });
        }
        

        
    }
}
