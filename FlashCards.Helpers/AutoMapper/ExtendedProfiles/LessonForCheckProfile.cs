using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class LessonForCheckProfile : Profile
    {
        public LessonForCheckProfile()
        {
            CreateMap<Lesson, LessonForCheck>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Flashcards, options => options.MapFrom(src => src.Flashcards));
        }
    }
}
