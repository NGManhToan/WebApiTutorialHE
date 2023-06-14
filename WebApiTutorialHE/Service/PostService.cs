using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Query;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;


namespace WebApiTutorialHE.Service
{
    public class PostService : IPostService
    {
        private readonly IPostQuery _postQuery;
        public PostService(IPostQuery postQuery)
        {
            _postQuery = postQuery;
        }


        public async Task<List<HomePostModel>> HomePost()
        {
            return await _postQuery.QueryHomePost();
        }
        public async Task<List<HomePostModel>> FindPost(string search)
        {
            return await _postQuery.QueryFindPost(search);
        }
        public async Task<List<HomePostModel>> GetPostFollowCategoryId(int id)
        {
            return await _postQuery.QuerySelectPostFollowCategoryId(id);
        }       
        public async Task<List<HomePostModel>> AscendPrice(int id)
        {
            return await _postQuery.QueryAscendPrice(id);
        }
        public async Task<List<HomePostModel>> DescendPrice(int id)
        {
            return await _postQuery.QueryDescendPrice(id);
        }
        public async Task<List<HomePostModel>> FilterFreeItem(int id)
        {
            return await _postQuery.QueryFilterFreeItem(id);
        }
        public async Task<List<HomePostModel>>GetDetailItem(int postId)
        {
            return await _postQuery.QueryGetDetailItem(postId); 
        }

        public async Task<List<HomeWishModel>> GetWishList()
        {
            return await _postQuery.QueryGetWishList();
        }
    }
}
