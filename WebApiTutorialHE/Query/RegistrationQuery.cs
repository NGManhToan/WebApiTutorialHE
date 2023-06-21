﻿using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService;
using WebApiTutorialHE.UtilsService.Interface;
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
        public async Task<List<RegistationListModel>> QueryGetListRegistation(int id)
        {
            var query = @"SELECT 
                            r.Id,
                            Title,
                            FullName,
                            CONCAT('" + Utils.LinkMedia("") + @"',
                                    m.ImageUrl) AS imageUrl,
                            r.Content,
                            r.Status
                          FROM
                            Post p
                                JOIN
                            Registration r ON p.Id = r.PostId
                                JOIN
                            User u ON p.CreatedBy = u.Id
                                LEFT JOIN
                            Media m ON p.Id = m.PostId
                          WHERE
                            r.IsDeleted = FALSE AND r.CreatedBy = @id";
            return await _sharingDapper.QueryAsync<RegistationListModel>(query, new {Id=id});
        }

        public async Task<int> QueryNumRegistation(int postId,int createdBy)
        {
            var query = @"select count(r.CreatedBy) as Num
                          from Registration r
		                        join User u on u.Id = r.CreatedBy
                                join Post p on p.Id = r.PostId
                          where r.PostId=@postId and  p.CreatedBy=@createdBy and r.IsDeleted = FALSE";
            return await _sharingDapper.QuerySingleAsync<int>(query, new
            {
                postId=postId,
                createdBy=createdBy
            });
        }
    }
}
