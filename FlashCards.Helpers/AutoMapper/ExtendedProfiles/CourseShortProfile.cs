using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class CourseShortProfile : Profile
    {
        public CourseShortProfile()
        {
            CreateMap<Course, CourseShort>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.AccountCreatedName, options => options.MapFrom(src => src.AccountCreated.DisplayName));
        }
    }
}
