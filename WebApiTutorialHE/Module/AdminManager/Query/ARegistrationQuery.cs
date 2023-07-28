using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Module.AdminManager.Model.Registration;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Query
{
    public class ARegistrationQuery:IARegistrationQuery
    {
        private readonly ISharingDapper _sharingDapper;

        public ARegistrationQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }

        public async Task<List<ARegistrationModel>> GetAllRegistration()
        {
            var query = @"SELECT 
                                r.Id, Title, FullName,u.StudentCode, r.Content, r.Status,r.CreatedDate,r.ApprovalDate
                            FROM
                                Post p
                                    JOIN
                                Registration r ON p.Id = r.PostId
                                    JOIN
                                User u ON p.CreatedBy = u.Id
                                    LEFT JOIN
                                Media m ON p.Id = m.PostId
                            WHERE
                                r.IsDeleted = FALSE";
            return await _sharingDapper.QueryAsync<ARegistrationModel>(query);
        }
    }
}
