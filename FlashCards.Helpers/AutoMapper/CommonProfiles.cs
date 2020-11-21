using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;

namespace FlashCards.Helpers.AutoMapper
{
    public class CommonProfiles : Profile
    {
        public CommonProfiles()
        {
            CreateMap<UserForRegister, User>();
            CreateMap<UserForRegister, UserInfo>();
            CreateMap<CourseForCreate, Course>();
            CreateMap<LessonForCreate, Lesson>();
            CreateMap<Lesson, LessonForList>();
            CreateMap<Flashcard, FlashcardForList>();
            CreateMap<FlashcardForCreate, Flashcard>();
            CreateMap<SubscribedCourse, SubscribedCourseShort>();
        }
    }
}
