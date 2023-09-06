using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Service
{
    public class AViolationReportService:IAViolationReportService
    {
        private readonly IAViolationReportQuery _aViolationReportQuery;
        private readonly IAViolationReportAction _aAction;

        public AViolationReportService(IAViolationReportQuery aViolationReportQuery,IAViolationReportAction aViolationReportAction)
        {
            _aViolationReportQuery = aViolationReportQuery;
            _aAction = aViolationReportAction;
        }

        public async Task<List<GetListViolationReportModel>> ListReport()
        {
            return await _aViolationReportQuery.ListReport();
        }

        public async Task<ViolationReport> EditViolationReport(int id, ForceInfo forceInfo)
        {
            return await _aAction.EditViolationReport(id, forceInfo);
        }
    }
}
