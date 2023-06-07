using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiTutorialHE.Database.SharingModels;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.UtilsProject;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApiTutorialHE.Action
{
    public class CategoryAction:ICategoryAction
    {
        private readonly SharingContext _sharingContext;
        public CategoryAction(SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }
        public async Task<Category> CreateActionCategory(CategoryListModel createcategory)
        {
            var category = new Category()
            {
                IdCategory = createcategory.id_category,
                NameCategory = createcategory.name_category,
                
            };
            _sharingContext.Add(category);
            await _sharingContext.SaveChangesAsync();
            return category;
        }
    }
}
