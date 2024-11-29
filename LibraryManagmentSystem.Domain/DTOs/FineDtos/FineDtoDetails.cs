
namespace LibraryManagmentSystem.Domain.DTOs.FineDtos
{
    public class FineDetailsDto : FineDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
