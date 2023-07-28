using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.Category;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ACategoryController : ControllerBase
    {
        private readonly IACategoryService _categoryService;

        public ACategoryController(IACategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult>AddCategory(ACategoryModel category)
        {
            var add = await _categoryService.AddCategoryService(category);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Them thanh cong",
                content = add
            });
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory(AEditCategoryModel editCategory)
        {
            if (editCategory.Name == null || editCategory.Name.Length == 0)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Tên vật phẩm không được để trống!",
                });
            }

            var update = await _categoryService.UpdateCategory(editCategory);
            if (update == null)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Chỉnh sửa không thành công",
                });
            }

            if (update.IsDeleted == true)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Chỉnh sửa không thành công",
                });
            }

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Chỉnh sửa thành công",
                content = update
            });
        }


        [HttpDelete]
        public async Task<IActionResult>DeleteCategory(int id)
        {
            var delete = await _categoryService.DeleteCategory(id);
            return Ok(delete);
        }
    }
}
