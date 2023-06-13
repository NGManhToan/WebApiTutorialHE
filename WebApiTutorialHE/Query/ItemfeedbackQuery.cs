using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Query
{
    public class ItemfeedbackQuery:IItemfeedbackQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public ItemfeedbackQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<ReceivedListModel>> GetItemfeedback() 
        {
            var query = @"SELECT p.Title,u.FullName,i.Content,
                            CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/',m.ImageUrl) as imageUrl
                            from post p join itemfeedback i on i.PostId=p.Id
                            join user u on p.CreatedBy= u.Id
                            join media m on p.CreatedBy=m.PostId
                            where i.IsDeleted=false";
            return await _sharingDapper.QueryAsync<ReceivedListModel>(query); 
        }
    }
}
