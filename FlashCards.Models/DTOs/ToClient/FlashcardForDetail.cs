namespace FlashCards.Models.DTOs.ToClient
{
    public class FlashcardForDetail
    {
        public int Id { get; set; }

        public string Phrase { get; set; }

        public string PhrasePronunciation { get; set; }

        public string PhraseSampleSentence { get; set; }

        public string PhraseComment { get; set; }

        public string TranslatedPhrase { get; set; }

        public string TranslatedPhraseSampleSentence { get; set; }

        public string TranslatedPhraseComment { get; set; }

        public string LanguageLevel { get; set; }

        public string Category { get; set; }
    }
}
