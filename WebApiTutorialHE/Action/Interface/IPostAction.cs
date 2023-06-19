using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Post;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IPostAction
    {
        Task<Comment> CreateCommet(CreateCommentModel createComment);

    }
}
