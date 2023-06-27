using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.User;

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
        Task<List<DetailItemModel>> GetDetailItem(int postId);
        Task<List<MySharingModel>> QueryGetShareListByUser(int id);
        Task<List<DetailWishListModel>> GetDetailWishList(int wishId);
        Task<List<CommentModel>> GetListComment(int postId);
        Task<Comment> CreateCommet(CreateCommentModel createComment);
        Task<Post>PostItem(PostItemModel postItemModel);
        Task<Post>PostProposal(PostProposalModel postProposalModel);
        Task<List<HomeWishModel>>GetWishListByUser(int userId);
        Task<Medium> PostImage(PostImgaeModel postImgae);


    }
}
