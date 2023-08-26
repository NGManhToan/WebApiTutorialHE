using System.ComponentModel;

namespace WebApiTutorialHE.Module.AdminManager.Model.User
{
    public class AdminUpdateManagerModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? StudentCode { get; set; }
        public int? FacultyId { get; set; } // Change to nullable int
        public string? Class { get; set; }

        public IFormFile? FileName { get; set; }
    }
}
