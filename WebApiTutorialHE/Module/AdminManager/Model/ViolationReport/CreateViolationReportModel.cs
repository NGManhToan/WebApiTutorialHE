using static System.Net.Mime.MediaTypeNames;

namespace WebApiTutorialHE.Module.AdminManager.Model.ViolationReport
{
    public class CreateViolationReportModel
    {
        public int PostId { get; set; }

        public string Message { get; set; }
    }
}
