using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain.Models
{
    public class Fine
    {
        public int Id { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fine amount cannot be negative.")]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }

        public int BorrowingId { get; set; }

        public Borrowing Borrowing { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
