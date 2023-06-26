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
        public async Task<List<ReceivedListModel>> GetItemfeedback(int id) 
        {
            var query = @"SELECT p.Title,u.FullName,i.Content,
								CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Post/',m.ImageUrl) as imageUrl
                           from ItemFeedback i
                           join Post p on p.Id=i.PostId
                           join User u on u.Id=i.CreatedBy
                           left join Media m on m.PostId=i.PostId
                            where i.IsDeleted=false and i.CreatedBy=@id";
            return await _sharingDapper.QueryAsync<ReceivedListModel>(query, new
            {
                id=id
            }); 
        }
    }
}
