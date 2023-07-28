namespace WebApiTutorialHE.Module.AdminManager.Model.User
{
    public class AdminUserListModel
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public int StudentCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public int Class { get; set; }
        public string UrlAvatar { get; set; }
        public string CreatedDate { get; set; }
    }
}
