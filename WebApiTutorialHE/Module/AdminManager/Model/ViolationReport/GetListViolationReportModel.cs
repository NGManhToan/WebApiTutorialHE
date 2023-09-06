namespace WebApiTutorialHE.Module.AdminManager.Model.ViolationReport
{
    public class GetListViolationReportModel
    {
        public int Id { get; set; }
        public string? ReporterFullName { get; set; } 

        public string StudentCode { get; set; }

        public string CreatedDate { get; set;}

        public string? PostId { get; set;}

        public string? Content { get; set;}

        public string CreatorFullName { get; set;}

        public string? ImageUrl { get; set;}

        public string? Description { get; set;}

        public int IsActive { get; set;}

        public string? ItemFeedbackId { get; set;}
        public string? CommentId { get; set;}
    }
}
