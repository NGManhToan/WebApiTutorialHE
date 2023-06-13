using System.ComponentModel;

namespace WebApiTutorialHE.Models.User
{
    public class UserReturnRegister
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Class { get; set; }
        public string StudentCode { get; set; }
        public int FacultyId { get; set; }
        [DefaultValue(1)]
        public int IsActive { get; set; }
        [DefaultValue(3)]
        public List<int> RoleIDs { get; set; }
        public string UrlAvatar { get; set; }
    }
}
