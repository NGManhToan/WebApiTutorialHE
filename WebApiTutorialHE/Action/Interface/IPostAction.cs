using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.Post;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IPostAction
    {
        Task<Comment> CreateCommet(CreateCommentModel createComment);
        Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata);
        Task<Post> PostItem(PostItemModel postItemModel);
    }
}
