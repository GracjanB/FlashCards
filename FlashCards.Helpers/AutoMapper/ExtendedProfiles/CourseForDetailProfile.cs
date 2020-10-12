using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class CourseForDetailProfile : Profile
    {
        public CourseForDetailProfile()
        {
            CreateMap<Course, CourseForDetail>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccountCreatedId, options => options.MapFrom(src => src.AccountCreatedId))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, options => options.MapFrom(src => src.Description))
                .ForMember(dest => dest.DateCreated, options => options.MapFrom(src => src.DateCreated.ToShortDateString()))
                .ForMember(dest => dest.DateModified, options => options.MapFrom(src => src.DateModified.ToShortDateString()))
                .ForMember(dest => dest.CourseType, options => options.MapFrom(src => src.CourseType))
                .ForMember(dest => dest.AmountOfEnrolled, options => options.MapFrom(src => src.CourseInfo.AmountOfEnrolled));
        }
    }
}
