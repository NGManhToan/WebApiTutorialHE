using System.ComponentModel.DataAnnotations;
using WebApiTutorialHE.Database.SharingModels;

namespace WebApiTutorialHE.Models.User
{
    public class UserListModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UrlAvatar { get; set; }
        public string IsOnline { get; set; }
        public int RoleId { get; set; }
        public string ClassCode { get; set; }
    }
}
