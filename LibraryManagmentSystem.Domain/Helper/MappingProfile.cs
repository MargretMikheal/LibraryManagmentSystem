using AutoMapper;
using LibraryManagementSystem.Domain.Models;
using LibraryManagmentSystem.Domain.DTOs;


namespace LibraryManagmentSystem.Domain.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Map ApplicationUser to UserDto for getting user information
            CreateMap<ApplicationUser, UserDto>();

            // Map UpdateUserDto to ApplicationUser, handling ProfilePicture separately
            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore()) // ProfilePicture is handled manually
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

