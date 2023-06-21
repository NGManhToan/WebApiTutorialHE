namespace WebApiTutorialHE.Models.Registation
{
    public class RegistrationProserModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string IsProposed { get; set; }
        public string TimeDiff { get; set; }
    }
}
