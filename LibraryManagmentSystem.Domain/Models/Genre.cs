using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "Genre name cannot exceed 30 characters.")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
