
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Domain.DTOs.BookDTOs
{
    public class BaseBookDto
    {
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public string Author { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN must be exactly 13 characters.")]
        public string ISBN { get; set; }

        public DateTime PublicationDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Copies available cannot be negative.")]
        public int GenreId { get; set; }
    }
}
