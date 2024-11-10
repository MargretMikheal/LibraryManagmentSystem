using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Total copies cannot be negative.")]
        public int TotalCopies { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available copies cannot be negative.")]
        public int AvailableCopies { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
