namespace WebApiTutorialHE.Module.AdminManager.Model.User
{
    public class ListUserAdminModel
    {
        public int id { get; set; }
        public string? FullName { get; set; }
        public string? StudentCode { get; set; }
        public string Class { get; set; }
        public string PhoneNumber { get; set; }
        public string UrlAvatar { get; set; }
        public int IsOnline { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
