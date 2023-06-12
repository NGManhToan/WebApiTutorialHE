using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        //[HttpPost]
        //public async Task<IActionResult>CreateCategory(CategoryListModel categoryListModel)
        //{
        //    var create= await _categoryService.CreateCategory(categoryListModel);
        //    return Ok(create);
        //}
        [HttpGet("export-excel")]
        public ActionResult ExportExcel()
        {
            var _empdata = _categoryService.GetDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_empdata, "Emloyee Records");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Category.xlsx");
                }
            }
        }
        [HttpGet("export-pdf")]
        public ActionResult ExportPDF()
        {
            var data = _categoryService.GetDatabase();

            using (MemoryStream ms = new MemoryStream())
            {
                // Tạo tài liệu PDF
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Tạo bảng PDF
                PdfPTable table = new PdfPTable(data.Columns.Count);
                table.WidthPercentage = 100;

                // Thêm tiêu đề cột
                foreach (DataColumn column in data.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                // Thêm dữ liệu từ DataTable vào bảng PDF
                foreach (DataRow row in data.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        table.AddCell(item.ToString());
                    }
                }

                // Thêm bảng vào tài liệu PDF
                document.Add(table);

                document.Close();

                // Trả về file PDF
                return File(ms.ToArray(), "application/pdf", "Category.pdf");
            }
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
