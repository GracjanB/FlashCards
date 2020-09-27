using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class CourseForListProfile : Profile
    {
        public CourseForListProfile()
        {
            CreateMap<Course, CourseForList>()

            .ForMember(dest => dest.Id, 
                options => options.MapFrom(src => src.Id))

            .ForMember(dest => dest.Name, 
                options => options.MapFrom(src => src.Name))

            .ForMember(dest => dest.Description, 
                options => options.MapFrom(src => src.Description))

            .ForMember(dest => dest.DateCreated, 
                options => options.MapFrom(src => src.DateCreated.ToShortDateString()))

            .ForMember(dest => dest.NumberOfEnrolled, 
                options => options.MapFrom(src => src.CourseInfo.AmountOfEnrolled))

            .ForMember(dest => dest.AuthorDisplayName, 
                options => options.MapFrom(src => src.AccountCreated.DisplayName))

            .ForMember(dest => dest.NumberOfRatings, 
                options => options.MapFrom(src => src.Opinions.Count))

            .ForMember(dest => dest.AverageRating, 
                options => options.MapFrom(src => src.Opinions.CalculateAverageRating()));
        }
    }
}
