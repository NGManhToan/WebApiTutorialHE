using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.util;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.UtilsService.Interface;
using DocumentFormat.OpenXml.Vml;
using Microsoft.AspNetCore.Authorization;

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
                Folder = System.IO.Path.Combine("wwwroot", "Upload", "Post"),
                FileName = "Image_Post",
                FormFile = image,
            };
            return await _cloudMediaService.SaveOneFileData(cloudOneMediaConfig);
        }

        [Authorize(Roles = "3")]
        public async Task<ReturnPostItemModel> PostItem(PostItemModel postItemModel, IFormFile fileName)
        {
            int maxFileSizeBytes = 5 * 1024 * 1024; // 5 MB
            if (fileName.Length > maxFileSizeBytes)
            {
                Results.BadRequest("Tệp đã vượt quá kích thước cho phép");
            }

            //string fileExtension = Path.GetExtension(fileName.FileName).ToLowerInvariant();
            //string[] acceptedImageFormats = { ".jpg", ".jpeg", ".png", ".gif" };
            //if (!acceptedImageFormats.Contains(fileExtension))
            //{
            //    Results.BadRequest("Định dạng tệp không được chấp nhận. Vui lòng tải lên hình ảnh có định dạng .jpg, .png, .gif, v.v.");
            //}

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

            var uploader = new Uploadfirebase();
            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await fileName.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            
            string imageUrl = await uploader.UploadPost(imageData, fileName.FileName);

            var postId = create.Id;
            var media = new Medium()
            {
                PostId = postId,
                ImageUrl = imageUrl,
            };
            _sharingContext.Media.Add(media);
            await _sharingContext.SaveChangesAsync();
            return new ReturnPostItemModel 
            {
                Title= create.Title,
                CreatedBy= create.CreatedBy,
                CategoryId= create.CategoryId,
                Content = create.Content,
                Price = create.Price,
                FromWishList= create.FromWishList,
                Status = create.Status,
                Type = create.Type,
                UrlImage = media.ImageUrl
            };
        }
        public async Task<Post>PostProposal(PostProposalModel postProposalModel, IFormFile fileName)
        {
            var create = new Post()
            {
                Title=postProposalModel.Title,
                Content=postProposalModel.Content,
                DesiredStatus=postProposalModel.DesiredStatus,
                CategoryId=postProposalModel.CategoryId,
                CreatedBy=postProposalModel.CreatedBy,
                Type = postProposalModel.Type,
                CreatedDate = Utils.DateNow(),
                LastModifiedBy=postProposalModel.CreatedBy
            };
            _sharingContext.Posts.Add(create);
            await _sharingContext.SaveChangesAsync();

            var uploader = new Uploadfirebase();
            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await fileName.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            string imageUrl = await uploader.UploadProposal(imageData, fileName.FileName);

            if (imageUrl != null && imageUrl.Length>0)
            {
                var postId = create.Id;
                var media = new Medium()
                {
                    PostId = postId,
                    ImageUrl = imageUrl
                };
                _sharingContext.Media.Add(media);
                await _sharingContext.SaveChangesAsync();
                
            }
            
            return create;
        }
        public async Task<Medium> PostImage(PostImgaeModel postImgae, string fileName)
        {
            var create = new Medium()
            {
                Id = postImgae.Id,
                PostId = postImgae.PostId,
                ImageUrl = fileName
            };
            _sharingContext.Media.Add(create);
            await _sharingContext.SaveChangesAsync();
            return create;
        }

        public async Task<Post> UpdateIssuccess(int id)
        {
            var proposed = await _sharingContext.Posts.FindAsync(id);
            if (proposed != null)
            {
                proposed.IsSuccess = true;
                _sharingContext.Posts.Update(proposed);

                var wishList = await _sharingContext.Posts.FindAsync(proposed.FromWishList);
                if (wishList != null)
                {
                    var createdBy = _sharingContext.Registrations
                                        .Where(x => x.PostId == id && x.CreatedBy == wishList.CreatedBy && !x.IsDeleted)
                                        .Where(x => x.Status.Equals("Accepted") || x.Status.Equals("Received"))
                                        .ToList();
                    if (createdBy != null && createdBy.Count > 0)
                    {
                        wishList.IsSuccess = true;
                        _sharingContext.Posts.Update(wishList);
                    }
                }

                await _sharingContext.SaveChangesAsync();

                return proposed;
            }
            return null;
        }

        public async Task<bool>DeleteWishList(int id)
        {
            var statusRegistrationList = new List<string> {
                "Disapproved", "Confirming"
            };
            var delete = await _sharingContext.Posts.FindAsync(id);

            if(delete != null && !delete.IsDeleted)
            {
                delete.IsDeleted = true;

                var proposed=_sharingContext.Posts.Where(x => x.FromWishList== id).ToList();
                foreach (var post in proposed)
                {
                    var register = await _sharingContext.Registrations.Where(x => x.PostId == post.Id
                                                                    && x.CreatedBy == delete.CreatedBy
                                                                    && !x.IsDeleted 
                                                                    && statusRegistrationList.Contains(x.Status)).FirstOrDefaultAsync();
                    if(register != null)
                    {
                        register.IsDeleted = true;
                        _sharingContext.Update(register);
                    }
                    
                }

                _sharingContext.Posts.Update(delete);

                await _sharingContext.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
