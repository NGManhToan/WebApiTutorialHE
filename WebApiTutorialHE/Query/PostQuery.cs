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
        public async Task<List<HomePostModel>> QuerySelectPostFollowCategoryId()
        {
            var query =
                @"SELECT Price, Title,
                    CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl
                  FROM Post p LEFT JOIN Category c ON p.CategoryId=c.Id
                                LEFT JOIN Media m ON p.Id=m.PostId WHERE CategoryId=1 AND type=1";
            //return await _sharingDapper.QueryAsync<HomePostModel>(query, new
            //{
            //    id = id
            //});
            return await _sharingDapper.QueryAsync<HomePostModel>(query);

        }
        public async Task<List<HomeWishModel>> QueryGetWishList()
        {
            var query= @"SELECT u.FullName, p.CreatedDate, Content,DesiredStatus,
                     CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl
                  FROM Post p left JOIN User u ON p.CreatedBy=u.Id
                                left JOIN Media m ON p.Id=m.PostId WHERE type=2";
            return await _sharingDapper.QueryAsync<HomeWishModel>(query);
        }
        public async Task<List<HomePostModel>> QueryAscendPrice()
        {
            var query= @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId=1  ORDER BY 
											CASE WHEN Price = 0 THEN 0 ELSE 1 END ASC,Price ASC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query);
        }
        public async Task<List<HomePostModel>> QueryDescendPrice()
        {
            var query = @"SELECT CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as ImageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId=1  ORDER BY 
											CASE WHEN Price = 0 THEN 0 ELSE 1 END DESC,Price DESC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query);
        }
        public async Task<List<HomePostModel>> QueryFilterFreeItem()
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
                  WHERE type=1 AND CategoryId=1
                            AND Price='Free';";
            return await _sharingDapper.QueryAsync<HomePostModel>(query);
        }
    }
}
