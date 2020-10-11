using System;
using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.DTOs.ToServer
{
    public class FlashcardForCreate
    {
        [Required(ErrorMessage = "Field is required")]
        [MinLength(2, ErrorMessage = "Minimum length for this field is 2 characters")]
        [MaxLength(64, ErrorMessage = "Maximum length for this field is 64 characters")]
        public string Phrase { get; set; }

        [MaxLength(64, ErrorMessage = "Maximum length for this field is 64 characters")]
        public string PhrasePronunciation { get; set; }

        [MaxLength(128, ErrorMessage = "Maximum length for this field is 128 characters")]
        public string PhraseSampleSentence { get; set; }

        [MaxLength(128, ErrorMessage = "Maximum length for this field is 128 characters")]
        public string PhraseComment { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MinLength(2, ErrorMessage = "Minimum length for this field is 2 characters")]
        [MaxLength(64, ErrorMessage = "Maximum length for this field is 64 characters")]
        public string TranslatedPhrase { get; set; }

        [MaxLength(128, ErrorMessage = "Maximum length for this field is 128 characters")]
        public string TranslatedPhraseSampleSentence { get; set; }

        [MaxLength(128, ErrorMessage = "Maximum length for this field is 128 characters")]
        public string TranslatedPhraseComment { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, 7, ErrorMessage = "Value of this field must be between 0 and 7")]
        public int LanguageLevel { get; set; }

        [MaxLength(64, ErrorMessage = "Maximum length for this field is 64 characters")]
        public string Category { get; set; }
    }
}
