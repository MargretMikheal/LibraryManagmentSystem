using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.DTOs.FineDtos
{
    public class FineDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public int BorrowingId { get; set; }
        public string UserId { get; set; }
    }
}
