using Microsoft.AspNetCore.Http;


namespace LibraryManagmentSystem.Domain.DTOs
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }

}
