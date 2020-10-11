using AutoMapper;
using FlashCards.Data.Enums;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToServer;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class CourseForCreateProfile : Profile
    {
        public CourseForCreateProfile()
        {
            CreateMap<CourseForCreate, Course>()

                .ForMember(dest => dest.Name,
                    options => options.MapFrom(x => x.Name))

                .ForMember(dest => dest.Description,
                    options => options.MapFrom(x => x.Description))

                .ForMember(dest => dest.CourseType,
                    options => options.MapFrom(x => (CourseTypeEnum)x.CourseType))

                .ForMember(dest => dest.AccountCreatedId,
                    options => options.MapFrom(x => x.AccountId));
        }
    }
}