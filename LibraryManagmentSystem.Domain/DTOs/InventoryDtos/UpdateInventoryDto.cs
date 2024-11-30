using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.DTOs.InventoryDtos
{
    public class UpdateInventoryDto
    {
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }
}
