namespace WebApiTutorialHE.Models.Account
{
    public class AccountExportModel
    {
        public int account_id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int type { get; set; }
    }
}
