namespace WebApiTutorialHE.Models.User
{
    public class UserUpdateModel
    {
        //public string? FullName { get; set; }
        //public string? StudentCode { get; set; }
        //public int? FacultyId { get; set; } // Change to nullable int
        //public string? Class { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        
        public IFormFile? fileName { get; set; }



    }
}
