using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;

namespace WebApiTutorialHE.Module.AdminManager.Service.Interface
{
    public interface IAViolationReportService
    {
        Task<List<GetListViolationReportModel>> ListReport();

        Task<ViolationReport> EditViolationReport(int id, ForceInfo forceInfo);
    }
}
