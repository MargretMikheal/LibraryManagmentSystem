using Microsoft.AspNetCore.Http;


namespace LibraryManagmentSystem.Domain.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }

}
