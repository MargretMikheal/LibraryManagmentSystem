using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN must be exactly 13 characters.")]
        public string ISBN { get; set; }  // ISBN unique constraint enforced at DB level

        [Range(0, int.MaxValue, ErrorMessage = "Copies available cannot be negative.")]
        public int CopiesAvailable { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }  // Navigation property
        public ICollection<Borrowing> Borrowings { get; set; }
        public Inventory Inventory { get; set; }
    }
}
