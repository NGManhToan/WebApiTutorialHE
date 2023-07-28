namespace WebApiTutorialHE.Models.UtilsProject
{
    public class LoginResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public Object content { get; set; }
    }
}
