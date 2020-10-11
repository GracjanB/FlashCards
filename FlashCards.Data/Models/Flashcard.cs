using FlashCards.Data.Enums;
using System;

namespace FlashCards.Data.Models
{
    public class Flashcard
    {
        public int Id { get; set; }

        public string Phrase { get; set; }

        public string PhrasePronunciation { get; set; }

        public string PhraseSampleSentence { get; set; }

        public string PhraseComment { get; set; }

        public string TranslatedPhrase { get; set; }

        public string TranslatedPhraseSampleSentence { get; set; }

        public string TranslatedPhraseComment { get; set; }

        public LanguageLevelEnum LanguageLevel { get; set; }

        public string Category { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }


        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }
    }
}
