using System.util;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Action
{
    public class PostAction:IPostAction
    {
        private readonly SharingContext _sharingContext;
        public PostAction (SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }
        public async Task<Comment> CreateCommet(CreateCommentModel createComment)
        {
            var comment = new Comment
            {
                Content = createComment.Content,
                CreatedBy=createComment.CreateBy,
                PostId=createComment.PostId,
                ParentCommentId=createComment.ParentCommentId,
                CreatedDate=Utils.DateNow(),
                LastModifiedBy=createComment.CreateBy,
                LastModifiedDate=Utils.DateNow(),
                IsDeleted=false,
                IsActive=true,
            };
             _sharingContext.Comments.Add(comment);
            await _sharingContext.SaveChangesAsync();
            return comment;
        }
    }
}
