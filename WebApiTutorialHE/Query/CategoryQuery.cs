using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Query
{
    public class CategoryQuery:ICategoryQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public CategoryQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<CategoryListModel>> QueryListCategory()
        {
            var query =
                @"SELECT Name 
                  FROM Category";
            return await _sharingDapper.QueryAsync<CategoryListModel>(query);
        }
       
    }
}
