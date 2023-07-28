using WebApiTutorialHE.Action;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Query;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;


namespace WebApiTutorialHE.Service
{
    public class PostService : IPostService
    {
        private readonly IPostQuery _postQuery;
        private readonly IPostAction _postAction;
        public PostService(IPostQuery postQuery, IPostAction postAction)
        {
            _postQuery = postQuery;
            _postAction = postAction;
        }


        public async Task<List<HomePostModel>> HomePost(int pageNumber, int pageSize)
        {
            return await _postQuery.QueryHomePost(pageNumber,pageSize);
        }
        public async Task<List<HomePostModel>> FindPost(string search)
        {
            return await _postQuery.QueryFindPost(search);
        }
        public async Task<List<HomePostModel>> GetPostFollowCategoryId(int id, int pageNumber, int pageSize)
        {
            return await _postQuery.QuerySelectPostFollowCategoryId(id,pageNumber,pageSize);
        }       
        public async Task<List<HomePostModel>> AscendPrice(int id)
        {
            return await _postQuery.QueryAscendPrice(id);
        }
        public async Task<List<HomePostModel>> DescendPrice(int id)
        {
            return await _postQuery.QueryDescendPrice(id);
        }
        public async Task<List<HomePostModel>> FilterFreeItem(int id)
        {
            return await _postQuery.QueryFilterFreeItem(id);
        }
        public async Task<List<DetailItemModel>>GetDetailItem(int postId)
        {
            return await _postQuery.QueryGetDetailItem(postId); 
        }

        public async Task<List<HomeWishModel>> GetWishList()
        {
            return await _postQuery.QueryGetWishList();
        }

        public async Task<List<MySharingModel>> QueryGetShareListByUser(int id)
        {
            return await _postQuery.QueryGetShareListByUser(id);
        }
        public async Task<List<DetailWishListModel>>GetDetailWishList(int wishId)
        {
            return await _postQuery.QueryDetailWishList(wishId);
        }
        public async Task<List<CommentModel>> GetListComment(int postId)
        {
            return await _postQuery.GetListComment(postId);
        }
        public async Task<Comment> CreateCommet(CreateCommentModel createComment)
        {
            return await _postAction.CreateCommet(createComment);
        }
        public async Task<ReturnPostItemModel> PostItem(PostItemModel postItemModel, IFormFile fileName)
        {
            //var file = await _postAction.SaveOneMediaData(postItemModel.UrlImage);
            return await _postAction.PostItem(postItemModel, fileName);
        }
        public async Task<Post>PostProposal(PostProposalModel postProposalModel, IFormFile fileName)
        {
            return await _postAction.PostProposal(postProposalModel, fileName);
        }
        public async Task<List<HomeWishModel>> GetWishListByUser(int userId)
        {
            return await _postQuery.GetWishListByUser(userId);
        }
        public async Task<Medium>PostImage(PostImgaeModel postImgae)
        {
            var file = await _postAction.SaveOneMediaData(postImgae.Image);
            if (file != null)
            {
                return await _postAction.PostImage(postImgae, file.FileName);
            }
            return null;
        }

        public async Task<Post> UpdateIssuccess(int id)
        {
            return await _postAction.UpdateIssuccess(id);
        }

        public async Task<bool> DeleteWishList(int id)
        {
            return await _postAction.DeleteWishList(id);
        }
    }
}
