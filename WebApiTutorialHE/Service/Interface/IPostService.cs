using WebApiTutorialHE.Models.Post;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IPostService
    {
        Task<List<HomePostModel>> HomePost();
        Task<List<HomePostModel>> FindPost(string search);
        Task<List<HomePostModel>> GetPostFollowCategoryId(int id);
        Task<List<HomeWishModel>> GetWishList();
        Task<List<HomePostModel>>AscendPrice(int id);
        Task<List<HomePostModel>> DescendPrice(int id);
        Task<List<HomePostModel>> FilterFreeItem(int id);
        Task<List<HomePostModel>> GetDetailItem(int postId);
    }
}
