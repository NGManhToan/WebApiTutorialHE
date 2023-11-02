using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;

namespace WebApiTutorialHE.Module.AdminManager.Service.Interface
{
    public interface IAViolationReportService
    {
        Task<List<GetListViolationReportModel>> ListReport();

        Task<List<GetListViolationReportModel>> ListReportIsFalse();

        Task<ViolationReport> EditViolationReport(int id, ForceInfo forceInfo);

        Task<ViolationReport> RemoveReport(int id, ForceInfo forceInfo);
    }
}
