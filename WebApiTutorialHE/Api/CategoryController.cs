using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly SharingContext _sharingContext;

        private readonly ICategoryService _categoryService;
        public CategoryController( ICategoryService _categoryService)
        {
            //_sharingContext = sharingContext;
            this._categoryService = _categoryService;
        }

        public static List<Category> Categories = new List<Category>();
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Catogorylist = await _categoryService.GetCategoryListModels();
            return Ok(Catogorylist);
        }
        [HttpPost]
        public async Task<IActionResult>CreateCategory(CategoryListModel categoryListModel)
        {
            var create= await _categoryService.CreateCategory(categoryListModel);
            return Ok(create);
        }
        
        //[HttpPost]
        //public IActionResult Create(CategoryListModel model)
        //{

        //    var category = new Category()
        //    {
        //        IdCategory = model.Id,
        //        NameCategory = model.NameCategory,
        //    };
        //    _sharingContext.Categories.Add(category);
        //    _sharingContext.SaveChanges();
        //    return Ok(category);
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var catogory = _sharingContext.Categories.FirstOrDefault(a => a.IdCategory == id);
        //    if (catogory == null)
        //    {
        //        return BadRequest("Không tìm thấy");
        //    }
        //    _sharingContext.Remove(catogory);
        //    _sharingContext.SaveChanges();
        //    return Ok("Đã xóa");
        //}

    }
}
