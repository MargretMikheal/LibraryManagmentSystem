using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.DTOs.InventoryDtos
{
    public class InventoryDetailsDto: InventoryDto
    {
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
