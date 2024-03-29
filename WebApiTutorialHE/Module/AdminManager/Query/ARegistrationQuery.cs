﻿using WebApiTutorialHE.Database.SharingModels;
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

        public async Task<List<CountRegistration>>CountRegistrationByCategory()
        {
            var query = @"Select c.Name as categoryName, count(r.Id) as quantityShared
                            from Post p
                              left join Registration r on p.Id = r.PostId
                               join Category c on c.Id = p.CategoryId
                             where r.Status = ""Received""
                            group by c.Name";
            return await _sharingDapper.QueryAsync<CountRegistration>(query);
        }

        public async Task<List<CountFaculty>> CountRegistrationByFaculty()
        {
            var query = @"Select f.Name as facultyName,count(r.Id) as quantityShared
                            from Post p
                              left join Registration r on p.Id = r.PostId
                               join User u on u.Id = p.CreatedBy
                               left join Faculty f on f.Id = u.FacultyId
                             where r.Status = ""Received""
                            group by f.Name";
            return await _sharingDapper.QueryAsync<CountFaculty>(query);
        }
    }
}
