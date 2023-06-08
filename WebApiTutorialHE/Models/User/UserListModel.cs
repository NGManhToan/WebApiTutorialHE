using System.ComponentModel.DataAnnotations;
using WebApiTutorialHE.Database.SharingModels;

namespace WebApiTutorialHE.Models.User
{
    public class UserListModel
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public List<UserRoleModel> roles { get; set; }
    }
}
