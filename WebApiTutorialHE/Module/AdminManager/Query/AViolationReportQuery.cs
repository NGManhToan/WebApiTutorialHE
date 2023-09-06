using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Query
{
    public class AViolationReportQuery:IAViolationReportQuery
    {
        private readonly ISharingDapper _sharingDapper;

        public AViolationReportQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }

        public async Task<List<GetListViolationReportModel>>ListReport()
        {
            var query = @"SELECT 
	                            vr.Id,
                                u.FullName as ReporterFullName, 
                                u.StudentCode, 
                                vr.CreatedDate, 
                                p.Id as PostId,
                                coalesce(p.Content,i.Content,c.Content) as Content,
                                (SELECT u2.FullName FROM User u2 WHERE u2.Id = p.CreatedBy or u2.Id = i.CreatedBy or u2.Id = c.CreatedBy ) AS CreatorFullName,
                                m.ImageUrl, 
                                vr.Description, 
                                vr.IsActive, 
                                CASE
                                    WHEN vr.ItemFeedbackId = 0 THEN NULL
                                    ELSE vr.ItemFeedbackId
                                END AS ItemFeedbackId,
                                CASE
                                    WHEN vr.CommentId = 0 THEN NULL
                                    ELSE vr.CommentId
                                END AS CommentId
                            FROM ViolationReport vr
                            JOIN User u ON u.Id = vr.CreatedBy
                            LEFT JOIN Post p ON p.Id = vr.PostId
                            LEFT JOIN Media m ON m.PostId = p.Id
                            LEFT JOIN ItemFeedback i on i.Id = vr.ItemFeedbackId
                            LEFT JOIN Comment c on c.Id = vr.CommentId
                            WHERE vr.IsDeleted = 0 AND vr.IsActive = 1 
                            ORDER BY vr.CreatedDate DESC;";
            return await _sharingDapper.QueryAsync<GetListViolationReportModel>(query);
        }
    }
}
