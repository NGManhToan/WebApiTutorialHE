﻿using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Models.Post;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Models.Registation;
using GrapeCity.Documents.Common;

namespace WebApiTutorialHE.Query
{
    public class PostQuery : IPostQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public PostQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<HomePostModel>> QueryHomePost(int pageNumber, int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;
            var query = @"
                            SELECT p.Id, m.imageUrl,
                            Title, CASE 
                                        WHEN Price = 0 THEN 'Free' 
                                        ELSE CAST(Price AS char(10)) 
                                    END AS Price
                            FROM Post p 
                            LEFT JOIN Media m ON p.Id = m.PostId 
                            WHERE type = 1 and p.IsActive = true
                            LIMIT @PageSize OFFSET @Offset";

            var parameters = new { PageSize = pageSize, Offset = offset };
            return await _sharingDapper.QueryAsync<HomePostModel>(query, parameters);
        }

        public async Task<List<HomePostModel>> QueryFindPost(string search)
        {
            var query = @"SELECT p.Id, m.imageUrl,
                    Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price FROM Post p LEFT JOIN Media m ON p.Id=m.PostId WHERE Title LIKE @search AND type=1";
            var parameters = new { Search = "%" + search + "%" };
            return await _sharingDapper.QueryAsync<HomePostModel>(query, parameters);
        }
        public async Task<List<HomePostModel>> QuerySelectPostFollowCategoryId(int id, int pageNumber, int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;
            var query =
                @"SELECT CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price,p.Id,p.CategoryId, Title,
                    m.imageUrl
                  FROM Post p LEFT JOIN Category c ON p.CategoryId=c.Id
                                LEFT JOIN Media m ON p.Id=m.PostId WHERE CategoryId LIKE @id AND type=1
                    LIMIT @PageSize OFFSET @Offset";

            //return await _sharingDapper.QueryAsync<HomePostModel>(query, new
            //{
            //    id = id
            //});
            var parameters = new { id = id,PageSize = pageSize, Offset = offset };
            return await _sharingDapper.QueryAsync<HomePostModel>(query,parameters);

        }
        public async Task<List<HomeWishModel>> QueryGetWishList()
        {
            var query = @"SELECT 
                            p.Id,
                            u.FullName,
                            p.CreatedDate,
                            p.Content,
                            CASE
                                WHEN p.DesiredStatus = 3 THEN 'Free, Purchase'
                                ELSE CAST(p.DesiredStatus AS CHAR(10))
                            END AS DesiredStatus,
                            m.imageUrl
                        FROM
                            Post p
                            LEFT JOIN User u ON p.CreatedBy = u.Id
                            LEFT JOIN Media m ON p.Id = m.PostId
                        WHERE
                            p.type = 2";
            return await _sharingDapper.QueryAsync<HomeWishModel>(query,new
            {
                Now = Utils.DateNow()
            });
        }
        public async Task<List<HomePostModel>> QueryAscendPrice(int id)
        {
            var query = @"SELECT p.Id, p.CategoryId ,m.imageUrl,
                    p.Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE Price 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id  ORDER BY 
											p.Price ASC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { id = id });
        }
        public async Task<List<HomePostModel>> QueryDescendPrice(int id)
        {
            var query = @"SELECT p.Id, p.CategoryId,m.imageUrl,
                    p.Title, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                  FROM Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id  ORDER BY 
											p.Price DESC;";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { id = id });   
        }
        public async Task<List<HomePostModel>> QueryFilterFreeItem(int id)
        {
            var query = @"SELECT p.Id,p.CategoryId,m.imageUrl,
                                Title, 
                                CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price
                            FROM 
                                Post p LEFT JOIN Media m ON p.Id=m.PostId
							LEFT JOIN Category c ON c.Id=p.CategoryId
                  WHERE type=1 AND CategoryId LIKE @id
                            AND p.Price='Free';";
            return await _sharingDapper.QueryAsync<HomePostModel>(query, new { id = id });
        }
        public async Task<List<DetailItemModel>> QueryGetDetailItem(int postId)
        {
            var query = @"select p.Id, Title, FullName, p.Status, Content, CASE 
                                    WHEN Price = 0 THEN 'Free' 
                                    ELSE CAST(Price AS char(10)) 
                                END AS Price, 
                    m.imageUrl
                    from Post p left join User u on p.CreatedBy=u.Id left join Media m on m.PostId=p.Id
                    where p.Id LIKE @postId and type=1";
            return await _sharingDapper.QueryAsync<DetailItemModel>(query, new { postId = postId });
        }

        public async Task<List<MySharingModel>> QueryGetShareListByUser(int id)
        {
            var query = @"select p.Id,p.Title,
		                        m.imageUrl
                        from Post p
	                         left join Media m on p.Id=m.PostId
	                         join User u on p.CreatedBy=u.Id
                        where p.CreatedBy=@id";
            return await _sharingDapper.QueryAsync<MySharingModel>(query, new { id = id });
        }
        public async Task<List<DetailWishListModel>> QueryDetailWishList(int wishId)
        {
            var query = @"select u.UrlAvatar ,FullName, p.CreatedDate, Content, c.Name, m.imageUrl,
                        CASE 
		                    WHEN DesiredStatus = 3 THEN 'Free, Purchase' 
                            ELSE CAST(DesiredStatus AS char(10))                             
                            END AS DesiredStatus
                    from User u left join Post p on u.Id=p.CreatedBy
			                    left join Category c on c.Id=p.CategoryId
                                left join Media m on m.PostId=p.Id
                    where p.Id = @wishId and type=2;";
            return await _sharingDapper.QueryAsync<DetailWishListModel>(query, new { wishId = wishId });
        }

        public async Task<List<CommentModel>> GetListComment(int postId)
        {
            var query = @"SELECT 
                            u.UrlAvatar,
                            u.FullName,
                            c.Content,
                            CASE 
                                WHEN TIMESTAMPDIFF(DAY, c.CreatedDate, NOW()) > 0 THEN CONCAT(TIMESTAMPDIFF(DAY, c.CreatedDate, NOW()), ' ngày trước')
                                WHEN TIMESTAMPDIFF(HOUR, c.CreatedDate, NOW()) > 0 THEN CONCAT(TIMESTAMPDIFF(HOUR, c.CreatedDate, NOW()), ' giờ trước')
                                ELSE CONCAT(TIMESTAMPDIFF(MINUTE, c.CreatedDate, NOW()), ' phút trước')
                            END AS TimeDiff
                        FROM 
                            Comment c
                            JOIN User u ON u.Id = c.CreatedBy
                            JOIN Post p ON p.Id = c.PostId
                        WHERE 
                            p.Id = @PostId";
            return await _sharingDapper.QueryAsync<CommentModel>(query, new
            {
                PostId = postId,
                Now = Utils.DateNow()
            });
        }
        public async Task<List<HomeWishModel>>GetWishListByUser(int userId)
        {
            var query = @"SELECT u.FullName, p.CreatedDate, Content,CASE 
		                    WHEN DesiredStatus = 3 THEN 'Free, Purchase' 
                            ELSE CAST(DesiredStatus AS char(10))                             
                            END AS DesiredStatus,
                     m.imageUrl
                  FROM Post p left JOIN User u ON p.CreatedBy=u.Id
                                left JOIN Media m ON p.Id=m.PostId WHERE type=2 and p.CreatedBy=@userId";
            return await _sharingDapper.QueryAsync<HomeWishModel>(query, new { userId = userId });
        }

        public async Task<List<PostItemShared>> GetListItemPostShared(int id)
        {
            var query = @"Select p.Id,p.Title, m.ImageUrl
                            From Post p
	                            join Registration r on p.Id = r.PostId
                                left join Media m on m.PostId = p.Id
                                join User u on u.Id = p.CreatedBy
                            where r.Status = 2 and u.Id = @id";
            return await _sharingDapper.QueryAsync<PostItemShared>(query, new
            {
                id = id
            });
        }

        public async Task<List<ReceivedItems>>ListReceivedItems(int id)
        {
            var query = @"Select m.ImageUrl,p.Title,u.FullName
                            from Post p
                             join Registration r on r.PostId = p.Id
                             join User u on u.Id = p.CreatedBy
                             left join Media m on m.PostId = p.Id
                             where r.Status=4 and u.Id =@id";
			return await _sharingDapper.QueryAsync<ReceivedItems>(query, new
			{
				id = id
			});
		}

		public async Task<QualityModel> CountRegistrationItem(int postId)
        {
            var query = @"Select count(*) as Quality
                            from Post p
                            join Registration r on r.PostId = p.Id
                            where r.PostId =@postId";
			return await _sharingDapper.QuerySingleAsync<QualityModel>(query, new
			{
				postId = postId
			});
		}

	}
}
