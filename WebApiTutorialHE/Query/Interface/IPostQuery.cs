using WebApiTutorialHE.Models.Post;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IPostQuery
    {
        Task<List<HomePostModel>> QueryHomePost();
        Task<List<HomePostModel>> QueryFindPost(string search);

        //Lấy ds các vật phẩm theo categoryId
        Task<List<HomePostModel>> QuerySelectPostFollowCategoryId();

        Task<List<HomeWishModel>>QueryGetWishList();
        Task<List<HomePostModel>> QueryAscendPrice();
        Task<List<HomePostModel>>QueryDescendPrice();
        Task<List<HomePostModel>> QueryFilterFreeItem();

    }
}
