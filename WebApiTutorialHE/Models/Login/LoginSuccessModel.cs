namespace WebApiTutorialHE.Models.Login
{
    public class LoginSuccessModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? UrlAvatar { get; set; }
        public string Roles { get; set; } = 3.ToString();
    }
}
