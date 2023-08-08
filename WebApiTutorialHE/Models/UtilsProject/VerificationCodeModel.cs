namespace WebApiTutorialHE.Models.UtilsProject
{
    public class VerificationCodeModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
