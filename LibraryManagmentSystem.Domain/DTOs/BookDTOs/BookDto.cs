using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.DTOs.BookDTOs
{
    public class BookDto:BaseBookDto
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
}
