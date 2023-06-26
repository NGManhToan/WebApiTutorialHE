using WebApiTutorialHE.Models.Category;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Database;
using System.Data;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface ICategoryService
    {
        Task<ObjectResponse> GetCategoryListModels();
        //Task<Category>CreateCategory(CategoryListModel category);
        DataTable GetDatabase();
    }
}
