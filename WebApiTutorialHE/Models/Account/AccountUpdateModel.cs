namespace WebApiTutorialHE.Models.Account
{
    public class AccountUpdateModel
    {
        public int account_id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
