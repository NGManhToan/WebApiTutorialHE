using System.ComponentModel.DataAnnotations;

namespace WebApiTutorialHE.Models.Account
{
    public class AccountListModel
    {
        public int account_id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int type { get; set; }
    }
}
    