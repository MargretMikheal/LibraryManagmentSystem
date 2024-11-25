using AutoMapper;
using LibraryManagementSystem.Domain.Models;
using LibraryManagmentSystem.Domain.DTOs;
using LibraryManagmentSystem.Domain.DTOs.BookDTOs;


namespace LibraryManagmentSystem.Domain.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();

            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore()) // ProfilePicture is handled manually
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Book, BookDto>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<AddBookDto, Book>();

            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

