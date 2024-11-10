using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowedDate { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime ReturnedDate { get; set; }

        public decimal FineAmount { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
        public ICollection<Fine> Fines { get; set; }
    }
}
