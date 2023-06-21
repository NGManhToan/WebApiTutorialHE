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


        public async Task<List<HomePostModel>> HomePost()
        {
            return await _postQuery.QueryHomePost();
        }
        public async Task<List<HomePostModel>> FindPost(string search)
        {
            return await _postQuery.QueryFindPost(search);
        }
        public async Task<List<HomePostModel>> GetPostFollowCategoryId(int id)
        {
            return await _postQuery.QuerySelectPostFollowCategoryId(id);
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
        public async Task<Post> PostItem(PostItemModel postItemModel)
        {
            var file = await _postAction.SaveOneMediaData(postItemModel.UrlImage);
            return await _postAction.PostItem(postItemModel, file.FileName);
        }
        public async Task<Post>PostProposal(PostProposalModel postProposalModel)
        {
            var file = await _postAction.SaveOneMediaData(postProposalModel.UrlImage);
            return await _postAction.PostProposal(postProposalModel, file.FileName);
        }
        public async Task<List<HomeWishModel>> GetWishListByUser(int userId)
        {
            return await _postQuery.GetWishListByUser(userId);
        }
    }
}
