using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController: ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public static List<Post> Categories = new List<Post>();
        [HttpGet("GetAllSharingItems")]
        public async Task<IActionResult> GetAll()
        {
            var homePost = await _postService.HomePost();
            return Ok(homePost);
        }

        [HttpGet("{search}")]///Tìm kiếm theo tên vật phẩm
        public async Task<IActionResult> FindPost(string search)
        {
            var findPost = await _postService.FindPost(search);
            return Ok(findPost);
        }

        [HttpGet]
        public async Task<IActionResult> GetPostFromCategoryId(int id)
        {
            var getPost = await _postService.GetPostFollowCategoryId(id);
            return Ok(getPost);
        }
        [HttpGet]
        public async Task<IActionResult> GetWishlish()
        {
            var getWish = await _postService.GetWishList();
            return Ok(getWish);
        }
        [HttpGet]
        public async Task<IActionResult> AscendPrice(int id)
        {
            var ascendPrice=await _postService.AscendPrice(id);
            return Ok(ascendPrice);
        }
        [HttpGet]
        public async Task<IActionResult> DescendPrice(int id)
        {
            var descendPrice = await _postService.DescendPrice(id);
            return Ok(descendPrice);
        }
        [HttpGet]
        public async Task<IActionResult> FilterFreeItem(int id)
        {
            var filterFreeItem = await _postService.FilterFreeItem(id);
            return Ok(filterFreeItem);
        }
        [HttpGet]
        public async Task<IActionResult>DetailItem(int postId)
        {
            var detailItem=await _postService.GetDetailItem(postId);
            return Ok(detailItem);
        }

        [HttpGet]
        public async Task<IActionResult> QueryGetShareListByUser(int id)
        {
            var getList=await _postService.QueryGetShareListByUser(id);
            return Ok(new ObjectResponse
            {
                result=1,
                message="Lấy thành công danh sách đăng theo người dùng",
                content = new
                {
                    getList= getList
                }
            });
        }
        [HttpGet]
        public async Task<IActionResult>DetailWishList(int wishId)
        {
            var detailWish = await _postService.GetDetailWishList(wishId);
            return Ok(detailWish);
        }
        [HttpGet]
        public async Task<IActionResult> GetListComment(int postId)
        {
            var getList= await _postService.GetListComment(postId);
            return  Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công danh sách comment theo PostId ",
                content = new
                {
                    getList = getList
                }
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentModel createComment)
        {
            var create = await _postService.CreateCommet(createComment);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Tạo Comment thành công",
                content = new
                {
                    create = create
                }
            });
        }
        [HttpPost]
        public async Task<IActionResult> PostItem([FromForm]PostItemModel postItemModel)
        {
            var postItem = await _postService.PostItem(postItemModel);
            return Ok(postItem);
        }
        [HttpPost]
        public async Task<IActionResult> PostProposal([FromForm] PostProposalModel postProposalModel)
        {
            var postProposal = await _postService.PostProposal(postProposalModel);
            return Ok(postProposal);
        }
        [HttpGet]
        public async Task<IActionResult> GetWishListByUser(int userId)
        {
            var getList = await _postService.GetWishListByUser(userId);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công danh sách đề xuất theo người dùng",
                content = new
                {
                    getList = getList
                }
            });
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id)
        {
            var update = await _postService.UpdateIssuccess(id);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Cập nhật thành công",
                content = new
                {
                    update = update
                }
            });
        }
        [HttpDelete]
        public async Task<IActionResult>DeleteWish(int id)
        {
            var delete = await _postService.DeleteWishList(id);
            return Ok(delete);
        }
    }
}
