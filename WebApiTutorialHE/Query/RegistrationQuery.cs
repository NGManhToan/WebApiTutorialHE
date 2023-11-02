using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService;
using WebApiTutorialHE.UtilsService.Interface;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Models.Post;

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
                             m.imageUrl,
                            r.Content,
                            r.Status,
                            r.CreatedDate,
							CASE
									WHEN p.Price = 0 THEN 'Free'
									ELSE CAST(p.Price AS CHAR (10))
								END AS DesiredStatus
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
        public async Task<List<RegistrationProserModel>> GetListRegistrationHaveProposer(int postId)
        {
            var query = @"select u.FullName,r.Content,u.Id, 
		                            IF(r.CreatedBy=p1.CreatedBy, 1, 0) IsProposed,
        	                        case  
                                        when timediff(@Now, r.CreatedDate)> '24:00:00' then concat(datediff(@Now, r.CreatedDate), ' ngày trước')
                                        when timediff(@Now, r.CreatedDate)> '1:00:00' then concat(hour(timediff(@Now, r.CreatedDate)), ' giờ trước')
                                        else concat(minute(timediff(@Now, r.CreatedDate)), ' phút trước')
                                        end TimeDiff
                            from Registration r
		                            join User u on u.Id=r.CreatedBy
                                    join Post p on p.Id=r.PostId
                                    left join Post p1 on p1.Id = p.FromWishList
                            where r.PostId=@PostId;";
            return await _sharingDapper.QueryAsync<RegistrationProserModel>(query, new
            {
                PostId = postId,
                Now = Utils.DateNow(),
            });
        }

        

    }
}
