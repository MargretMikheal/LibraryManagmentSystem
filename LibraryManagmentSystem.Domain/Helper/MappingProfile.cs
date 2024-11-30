using AutoMapper;
using LibraryManagementSystem.Domain.Models;
using LibraryManagmentSystem.Domain.DTOs.BookDTOs;
using LibraryManagmentSystem.Domain.DTOs.BorrowingDTos;
using LibraryManagmentSystem.Domain.DTOs.FineDtos;
using LibraryManagmentSystem.Domain.DTOs.GenreDtos;
using LibraryManagmentSystem.Domain.DTOs.InventoryDtos;
using LibraryManagmentSystem.Domain.DTOs.UserDtos;


namespace LibraryManagmentSystem.Domain.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();

            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore()) 
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Book, BookDto>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<AddBookDto, Book>();

            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<Genre,GetGenreDto>().ReverseMap();
            CreateMap<GenreDto, Genre>();

            CreateMap<Borrowing, BorrowingDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<CreateBorrowingDto, Borrowing>();

            
                CreateMap<Fine, FineDto>().ReverseMap();
                CreateMap<Fine, FineDetailsDto>()
                    .ForMember(dest => dest.CreatedAt,
                               opt => opt.MapFrom(src => src.Borrowing.ReturnedDate));

            CreateMap<Inventory, InventoryDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));

            CreateMap<Inventory, InventoryDetailsDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Book.Author))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Book.Genre.Name));

            CreateMap<UpdateInventoryDto, Inventory>();

        }
    }
}

