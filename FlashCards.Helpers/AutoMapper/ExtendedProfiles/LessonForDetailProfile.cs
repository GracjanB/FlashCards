using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class LessonForDetailProfile : Profile
    {
        public LessonForDetailProfile()
        {
            CreateMap<Lesson, LessonForDetail>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, options => options.MapFrom(src => src.Description))
                .ForMember(dest => dest.Category, options => options.MapFrom(src => src.Category))
                .ForMember(dest => dest.DateCreated, options => options.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.DateModified, options => options.MapFrom(src => src.DateModified))
                .ForMember(dest => dest.Flashcards, options => options.MapFrom(src => src.Flashcards));
        }
    }
}
