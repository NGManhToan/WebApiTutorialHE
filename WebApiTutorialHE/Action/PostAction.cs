using System.util;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Action
{
    public class PostAction:IPostAction
    {
        private readonly SharingContext _sharingContext;
        private readonly ICloudMediaService _cloudMediaService;
        public PostAction (SharingContext sharingContext, ICloudMediaService cloudMediaService)
        {
            _sharingContext = sharingContext;
            _cloudMediaService = cloudMediaService;
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
        public async Task<CloudOneMediaModel> SaveOneMediaData(IFormFile image)
        {
            var cloudOneMediaConfig = new CloudOneMediaConfig
            {
                Folder = Path.Combine("wwwroot", "Upload", "Post"),
                FileName = "Image_Post",
                FormFile = image,
            };
            return await _cloudMediaService.SaveOneFileData(cloudOneMediaConfig);
        }
        public async Task<Post> PostItem(PostItemModel postItemModel, string fileName)
        {
            
            var create = new Post()
            {
                Title = postItemModel.Title,
                CategoryId = postItemModel.CategoryId,
                Status = postItemModel.Status,
                Price = postItemModel.Price,
                Content = postItemModel.Content,
                Type = postItemModel.Type,
                CreatedBy = postItemModel.CreatedBy,
                CreatedDate = Utils.DateNow(),
                LastModifiedBy = postItemModel.CreatedBy,
                FromWishList = postItemModel.FromWishList,
                
            };
            _sharingContext.Posts.Add(create);
            await _sharingContext.SaveChangesAsync();
            var postId = create.Id;
            var media = new Medium()
            {
                PostId = postId,
                ImageUrl = fileName
            };
            _sharingContext.Media.Add(media);
            await _sharingContext.SaveChangesAsync();
            return create;
        }
    }
}
