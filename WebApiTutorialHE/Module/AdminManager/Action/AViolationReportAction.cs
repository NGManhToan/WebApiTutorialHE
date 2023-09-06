using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiTutorialHE.Module.AdminManager.Action
{
    public class AViolationReportAction:IAViolationReportAction
    {
        private readonly SharingContext _sharingContext;

        public AViolationReportAction (SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
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
    }
}
