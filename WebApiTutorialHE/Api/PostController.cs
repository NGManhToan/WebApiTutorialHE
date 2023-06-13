using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]")]
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

        [HttpGet("/GetCategoryId")]
        public async Task<IActionResult> GetPostFromCategoryId()
        {
            var getPost = await _postService.GetPostFollowCategoryId();
            return Ok(getPost);
        }
        [HttpGet("GetWishlist")]
        public async Task<IActionResult> GetWishlish()
        {
            var getWish = await _postService.GetWishList();
            return Ok(getWish);
        }
    }
}
