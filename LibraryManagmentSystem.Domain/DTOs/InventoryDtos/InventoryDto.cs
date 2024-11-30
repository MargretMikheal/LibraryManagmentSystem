using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.DTOs.InventoryDtos
{
    
    public class InventoryDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }
    

}
