using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService;
using WebApiTutorialHE.UtilsService.Interface;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Query
{
    public class RegistrationQuery: IRegistrationQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public RegistrationQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<RegistationListModel>> QueryGetListRegistation()
        {
            var query = @"SELECT 
                            Title,FullName,
                            CONCAT('" + Utils.LinkMedia("") + @"', 'Upload/Avatar/Avatar_/',p.ImageUrl) imageUrl,
                            r.Content,
                            r.Status
                          FROM
                            post p
                                JOIN
                            registation r ON p.Id = r.PostId
                                JOIN
                            user u ON p.CreatedBy = u.Id";
            return await _sharingDapper.QueryAsync<RegistationListModel>(query);
        }
    }
}
