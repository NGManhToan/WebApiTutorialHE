using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;

namespace WebApiTutorialHE.Module.AdminManager.Action.Interface
{
    public interface IAViolationReportAction
    {
        Task<ViolationReport> EditViolationReport(int id, ForceInfo forceInfo);

        Task<ViolationReport> DeleteViolationReport(int id, ForceInfo forceInfo);
    }
}
