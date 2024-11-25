namespace LibraryManagmentSystem.Domain.DTOs.BookDTOs
{
    public class UpdateBookDto
    {
        public DateTime? PublicationDate { get; set; }
        public int? CopiesAvailable { get; set; }
        public int? GenreId { get; set; }
    }
}
