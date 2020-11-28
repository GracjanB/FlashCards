namespace FlashCards.Models.DTOs.ToClient
{
    public class FlashcardForList
    {
        public int Id { get; set; }

        public string Phrase { get; set; }

        public string TranslatedPhrase { get; set; }

        public bool IsSubscribed { get; set; }

        public decimal Progress { get; set; }

        public bool MarkedAsHard { get; set; }
    }
}
