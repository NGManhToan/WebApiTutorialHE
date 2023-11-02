using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;

namespace WebApiTutorialHE.Module.AdminManager.Query.Interface
{
    public interface IAViolationReportQuery
    {
        Task<List<GetListViolationReportModel>> ListReport();

        Task<List<GetListViolationReportModel>> ListReporIsActiveFalse();
    }
}
