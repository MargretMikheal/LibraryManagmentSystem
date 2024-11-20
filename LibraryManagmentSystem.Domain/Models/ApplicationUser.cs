using LibraryManagmentSystem.Domain.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(15, ErrorMessage = "First name cannot exceed 15 characters.")]
        public string FirstName { get; set; }

        [StringLength(15, ErrorMessage = "Last name cannot exceed 15 characters.")]
        public string LastName { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; }  // Every user can borrow many books
        public ICollection<Fine> Fines { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
