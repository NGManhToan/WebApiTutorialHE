using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.Registation;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IPostQuery
    {
        Task<List<HomePostModel>> QueryHomePost(int pageNumber, int pageSize);
        Task<List<HomePostModel>> QueryFindPost(string search);

        //Lấy ds các vật phẩm theo categoryId
        Task<List<HomePostModel>> QuerySelectPostFollowCategoryId(int id, int pageNumber, int pageSize);

        Task<List<HomeWishModel>>QueryGetWishList();
        Task<List<HomePostModel>> QueryAscendPrice(int id);
        Task<List<HomePostModel>>QueryDescendPrice(int id);
        Task<List<HomePostModel>> QueryFilterFreeItem(int id);
        Task<List<DetailItemModel>> QueryGetDetailItem(int postId);
        Task<List<MySharingModel>> QueryGetShareListByUser(int id);
        Task<List<DetailWishListModel>>QueryDetailWishList(int wishId);
        Task<List<CommentModel>> GetListComment(int postId);
        Task<List<HomeWishModel>> GetWishListByUser(int userId);

        Task<List<PostItemShared>> GetListItemPostShared(int id);

        Task<List<ReceivedItems>> ListReceivedItems(int id);
        Task<QualityModel> CountRegistrationItem(int postId);
	}
}
