using Dapper;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;
using WebApiTutorialHE.Service;

namespace WebApiTutorialHE.Module.AdminManager.Action
{
    public class AViolationReportAction:IAViolationReportAction
    {
        private readonly SharingContext _sharingContext;
        private readonly SharingHub _sharingHub;

        public AViolationReportAction (SharingContext sharingContext,SharingHub sharingHub)
        {
            _sharingContext = sharingContext;
            _sharingHub = sharingHub;
        }

        public async Task<ViolationReport> EditViolationReport(int id, ForceInfo forceInfo)
        {

            var editReport = await _sharingContext.ViolationReports.FindAsync(id);
            if (editReport == null)
            {
                throw new BadHttpRequestException($"Không có {id} tồn tại !");
            }
            else
            {
                editReport.IsActive = false;
                editReport.LastModifiedDate = Utils.DateNow();
                editReport.CreatedBy = forceInfo.UserId;

                _sharingContext.ViolationReports.Update(editReport);
                await _sharingContext.SaveChangesAsync();
            }
            return editReport;
        }

        public async Task<ViolationReport> DeleteViolationReport(int id, ForceInfo forceInfo)
        {
            var report = await _sharingContext.ViolationReports.FindAsync(id);
            if (report != null && !report.IsDeleted && report.IsActive == true)
            {
                report.IsActive = false; //Da xu ly roi
                await _sharingContext.SaveChangesAsync();
                if (report.PostId != null)
                {
                    var post = await _sharingContext.Posts.FindAsync(report.PostId);
                    if (post != null)
                    {
                        post.IsDeleted= true;
                        await _sharingContext.SaveChangesAsync();
                    }
                }
                else if (report.ItemFeedbackId != null)
                {
                    var itemFeedback = await _sharingContext.ItemFeedbacks.FindAsync(report.ItemFeedbackId);
                    if (itemFeedback != null)
                    {
                        itemFeedback.IsDeleted= true;
                        await _sharingContext.SaveChangesAsync();
                    }
                }
                else if (report.CommentId != null)
                {
                    var comment = await _sharingContext.Comments.FindAsync(report.CommentId);
                    if (comment != null)
                    {
                        comment.IsDeleted= true;
                        await _sharingContext.SaveChangesAsync();
                    }
                }


                //    var query = @"SELECT 
                //                    COALESCE(p.CreatedBy, i.CreatedBy, c.CreatedBy) AS CreatorCreateBy
                //                FROM ViolationReport vr
                //                JOIN User u ON u.Id = vr.CreatedBy
                //                LEFT JOIN Post p ON p.Id = vr.PostId
                //                LEFT JOIN ItemFeedback i on i.Id = vr.ItemFeedbackId
                //                LEFT JOIN Comment c on c.Id = vr.CommentId
                //                WHERE vr.IsDeleted = 0 AND vr.IsActive = 1 
                //                ORDER BY vr.CreatedDate DESC";

                //var createrIds = (await _sharingContext.Database.GetDbConnection().QueryAsync<int>(query)).ToList();
                




                //var reporterId = report.CreatedBy;

                //// Gửi tin nhắn đến người báo cáo và người bị báo cáo
                //var toUserIds = new List<int> { reporterId};
                //toUserIds.AddRange(createrIds);


                //var notification = new Notification
                //{
                //    Content = "Test notification",
                //    CreatedBy = forceInfo.UserId,
                //    LastModifiedDate = forceInfo.DateNow
                    
                //};

                //// Lưu tin nhắn vào cơ sở dữ liệu
                //_sharingContext.Notifications.Add(notification);
                //await _sharingContext.SaveChangesAsync();

                //await _sharingHub.SendMessOnUser(toUserIds, notification);
            }
            else
            {
                throw new Exception("Không tìm thấy báo cáo vi phạm có ID tương ứng.");
            }

            return report;
        }


        //var query = from vr in _sharingContext.ViolationReports
        //            join u in _sharingContext.Users on vr.CreatedBy equals u.Id
        //            join p in _sharingContext.Posts on vr.PostId equals p.Id into pGroup
        //            from p in pGroup.DefaultIfEmpty()
        //            join i in _sharingContext.ItemFeedbacks on vr.ItemFeedbackId equals i.Id into iGroup
        //            from i in iGroup.DefaultIfEmpty()
        //            join c in _sharingContext.Comments on vr.CommentId equals c.Id into cGroup
        //            from c in cGroup.DefaultIfEmpty()
        //            where !vr.IsDeleted && vr.IsActive.GetValueOrDefault()
        //            orderby vr.CreatedDate descending
        //            select new
        //            {
        //                ReporterFullName = u.FullName,
        //                CreatorFullName = _sharingContext.Users.Where(u2 => u2.Id == p.CreatedBy || u2.Id == i.CreatedBy || u2.Id == c.CreatedBy).Select(u2 => u2.FullName).FirstOrDefault()
        //            };

    }
}
