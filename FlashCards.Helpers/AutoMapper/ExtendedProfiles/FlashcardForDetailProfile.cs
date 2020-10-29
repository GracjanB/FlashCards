using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Helpers.AutoMapper.ExtendedProfiles
{
    public class FlashcardForDetailProfile : Profile
    {
        public FlashcardForDetailProfile()
        {
            CreateMap<Flashcard, FlashcardForDetail>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Phrase, options => options.MapFrom(src => src.Phrase))
                .ForMember(dest => dest.PhrasePronunciation, options => options.MapFrom(src => src.PhrasePronunciation))
                .ForMember(dest => dest.PhraseSampleSentence, options => options.MapFrom(src => src.PhraseSampleSentence))
                .ForMember(dest => dest.PhraseComment, options => options.MapFrom(src => src.PhraseComment))
                .ForMember(dest => dest.TranslatedPhrase, options => options.MapFrom(src => src.TranslatedPhrase))
                .ForMember(dest => dest.TranslatedPhraseSampleSentence, options => options.MapFrom(src => src.TranslatedPhraseSampleSentence))
                .ForMember(dest => dest.TranslatedPhraseComment, options => options.MapFrom(src => src.TranslatedPhraseComment))
                .ForMember(dest => dest.LanguageLevel, options => options.MapFrom(src => src.LanguageLevel.GetDescription()))
                .ForMember(dest => dest.Category, options => options.MapFrom(src => src.Category));
        }
    }
}
