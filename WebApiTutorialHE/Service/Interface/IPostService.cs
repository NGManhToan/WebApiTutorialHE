using WebApiTutorialHE.Models.Post;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IPostService
    {
        Task<List<HomePostModel>> HomePost();
        Task<List<HomePostModel>> FindPost(string search);
        Task<List<HomePostModel>> GetPostFollowCategoryId();
        Task<List<HomeWishModel>> GetWishList();
        Task<List<HomePostModel>>AscendPrice();
        Task<List<HomePostModel>> DescendPrice();
        Task<List<HomePostModel>> FilterFreeItem();
    }
}
