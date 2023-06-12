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
        [HttpGet]
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

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetPostFromCategoryId(int id)
        {
            var getPost = await _postService.GetPostFollowCategoryId(id);
            return Ok(getPost);
        }
    }
}
