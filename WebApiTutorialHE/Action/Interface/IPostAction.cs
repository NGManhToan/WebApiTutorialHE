using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.Post;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IPostAction
    {
        Task<Comment> CreateCommet(CreateCommentModel createComment);
        Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata);
        Task<ReturnPostItemModel> PostItem(PostItemModel postItemModel, IFormFile fileName);
        Task<Post>PostProposal(PostProposalModel postProposalModel, IFormFile fileName);
        Task<Medium> PostImage(PostImgaeModel postImgae, string fileName);
        Task<Post> UpdateIssuccess(int id);
        Task<bool> DeleteWishList(int id);
    }
}
