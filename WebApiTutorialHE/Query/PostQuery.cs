using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

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
                @"SELECT ImageUrl, Title
                  FROM post where status is null";
            return await _sharingDapper.QueryAsync<HomePostModel>(query);
        }
        public async Task<List<HomePostModel>> QueryFindPost(string search)
        {
            var query = @"SELECT ImageUrl, Title FROM post WHERE Title LIKE @search";
            var parameters = new { Search = "%" + search + "%" };
            return await _sharingDapper.QueryAsync<HomePostModel>(query, parameters);
        }
        public async Task<List<HomePostModel>> QuerySelectPostFollowCategoryId(int id)
        {
            var query =
                @"SELECT ImageUrl, Title
                  FROM post  WHERE CategoryId=@id";
            //var parameters = new { Id=id };
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new
            {
                id = id
            });
        }
    }
}
