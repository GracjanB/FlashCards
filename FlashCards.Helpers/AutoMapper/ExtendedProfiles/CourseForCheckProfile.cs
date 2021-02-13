using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class CourseForCheckProfile : Profile
    {
        public CourseForCheckProfile()
        {
            CreateMap<Course, CourseForCheck>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, options => options.MapFrom(src => src.Description))
                .ForMember(dest => dest.DateCreated, options => options.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.AccountCreatedName, options => options.MapFrom(src => src.AccountCreated.DisplayName))
                .ForMember(dest => dest.Lessons, options => options.MapFrom(src => src.Lessons));
        }
    }
}
