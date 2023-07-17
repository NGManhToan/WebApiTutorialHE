using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.User;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IPostService
    {
        Task<List<HomePostModel>> HomePost(int pageNumber, int pageSize);
        Task<List<HomePostModel>> FindPost(string search);
        Task<List<HomePostModel>> GetPostFollowCategoryId(int id);
        Task<List<HomeWishModel>> GetWishList();
        Task<List<HomePostModel>>AscendPrice(int id);
        Task<List<HomePostModel>> DescendPrice(int id);
        Task<List<HomePostModel>> FilterFreeItem(int id);
        Task<List<DetailItemModel>> GetDetailItem(int postId);
        Task<List<MySharingModel>> QueryGetShareListByUser(int id);
        Task<List<DetailWishListModel>> GetDetailWishList(int wishId);
        Task<List<CommentModel>> GetListComment(int postId);
        Task<Comment> CreateCommet(CreateCommentModel createComment);
        Task<ReturnPostItemModel> PostItem(PostItemModel postItemModel, IFormFile fileName);
        Task<Post>PostProposal(PostProposalModel postProposalModel, IFormFile fileName);
        Task<List<HomeWishModel>>GetWishListByUser(int userId);
        Task<Medium> PostImage(PostImgaeModel postImgae);
        Task<Post> UpdateIssuccess(int id);
        Task<bool> DeleteWishList(int id);


    }
}
