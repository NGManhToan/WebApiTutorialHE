using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController:ControllerBase
    {
        private readonly IShowImageSevice _showImage;
        public ImageController(IShowImageSevice showImage)
        {
            _showImage = showImage;
        }
        [HttpGet]
        public async Task<IActionResult> GetImage(string fileName)
        {
            var file = await _showImage.ShowImageAsync(fileName);
            return new FileContentResult(file, "image/jpeg");
        }
    }
}
