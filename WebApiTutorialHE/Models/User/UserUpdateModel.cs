namespace WebApiTutorialHE.Models.User
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? StudentCode { get; set; }
        public int? FacultyId { get; set; } // Change to nullable int
        public string? Class { get; set; }
        public IFormFile? UrlImage { get; set; }
    }
}
