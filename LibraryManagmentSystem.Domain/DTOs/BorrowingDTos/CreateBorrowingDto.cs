using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.DTOs.BorrowingDTos
{
    public class CreateBorrowingDto
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
    }
}
