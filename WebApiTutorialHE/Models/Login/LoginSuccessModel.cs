namespace WebApiTutorialHE.Models.Login
{
    public class LoginSuccessModel
    {
        public int user_id { get; set; }
        public string full_name { get; set; }
        public string student_code { get; set; }
        public string phone_number{ get; set; }
        public string Class { get; set; }
        public int Type { get; set; }
    }
}
