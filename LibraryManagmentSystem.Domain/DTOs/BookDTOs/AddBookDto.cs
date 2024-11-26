using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.DTOs.BookDTOs
{
    public class AddBookDto:BaseBookDto 
    {
       public int TotalCopies { get; set; }
    }
}
