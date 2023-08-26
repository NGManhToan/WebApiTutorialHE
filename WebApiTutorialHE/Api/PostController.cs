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
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var homePost = await _postService.HomePost(pageNumber,pageSize);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lay thanh cong",
                content = homePost
            });
        }

        [HttpGet("{search}")]///Tìm kiếm theo tên vật phẩm
        public async Task<IActionResult> FindPost(string search)
        {
            var findPost = await _postService.FindPost(search);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Tìm thành công",
                content = findPost
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetPostFromCategoryId(int id, int pageNumber, int pageSize)
        {
            var getPost = await _postService.GetPostFollowCategoryId(id,pageNumber,pageSize);
            return Ok(new ObjectResponse
            {
                result=1,
                message = "Lay thanh cong",
                content = getPost
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetWishlish()
        {
            var getWish = await _postService.GetWishList();
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công",
                content = getWish
            });
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
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công chi tiết",
                content = detailItem
            });
        }

        [HttpGet]
        public async Task<IActionResult> QueryGetShareListByUser()
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = DateTime.Now,
            };
            if(forceInfo.UserId == 0)
            {
                return Ok(new ObjectResponse { result = 0, message = "Xác thực không thành công" });
            }
            var getList = await _postService.QueryGetShareListByUser(forceInfo.UserId);
            if(getList != null)
            {
                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Lấy danh sách thành công !",
                    content = getList
                });
            }
            else
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Lấy danh sách không thành công !!"
                });
            }
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
        public async Task<IActionResult> PostItem([FromForm]PostItemModel postItemModel, IFormFile fileName)
        {
            var postItem = await _postService.PostItem(postItemModel,fileName);
            return Ok(postItem);
        }
        [HttpPost]
        public async Task<IActionResult> PostProposal([FromForm] PostProposalModel postProposalModel, IFormFile fileName)
        {
            var postProposal = await _postService.PostProposal(postProposalModel, fileName);
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
