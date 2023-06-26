using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemfeedbackController:ControllerBase
    {
        private readonly IItemFeedbackService _itemfeedbackService;
        public ItemfeedbackController(IItemFeedbackService itemfeedbackService)
        {
            _itemfeedbackService = itemfeedbackService;
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var itemFeedback = await _itemfeedbackService.GetByUser(id);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy danh sách thành công",
                content = new
                {
                    itemFeedback = itemFeedback
                }
            });
        }
        [HttpPut]
        public async Task<IActionResult>UpdateItemfeedback(ItemfeedbackUpdateModel updateModel)
        {
            var update = await _itemfeedbackService.Updateitemfeedback(updateModel);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Cập nhật thành công",
                content = new
                {
                   Itemfeedback  = update
                }
            });
        }
        [HttpDelete]
        public async Task<IActionResult> Deleteitemfeedback(int id)
        {
            var itemFeedback = await _itemfeedbackService.Deleteitemfeedback(id);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Xóa thành công",
                content = new
                {
                    itemFeedback = itemFeedback
                }
            });
        }
    }
}
