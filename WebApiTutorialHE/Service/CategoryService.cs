using System.Data;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryQuery _categoryQuery;
        private readonly ICategoryAction _categoryAction;
        private readonly SharingContext _sharingContext;

        public CategoryService(ICategoryQuery categoryQuery,ICategoryAction  categoryAction, SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
            _categoryQuery = categoryQuery;
            _categoryAction = categoryAction;
        }
         
        public async Task<List<CategoryListModel>> GetCategoryListModels()
        {
            return await _categoryQuery.QueryListCategory();
        }

        //public async Task<Category> CreateCategory(CategoryListModel category)
        //{
        //    return await _categoryAction.CreateActionCategory(category);
        //}
        public DataTable GetDatabase()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("createdby", typeof(int));
            dt.Columns.Add("isdeleted", typeof(bool));
            dt.Columns.Add("isactive", typeof(bool));
            dt.Columns.Add("createddate", typeof(DateTime));
            dt.Columns.Add("lastmodifileddate", typeof(DateTime));
            dt.Columns.Add("lastmodifileby", typeof(int));

            var _list = this._sharingContext.Categories.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.Name, item.CreatedBy, item.IsDeleted,item.IsActive,
                        item.CreatedDate, item.LastModifiedDate, item.LastModifiedBy);
                });
            }
            return dt;
        }
    }
}
