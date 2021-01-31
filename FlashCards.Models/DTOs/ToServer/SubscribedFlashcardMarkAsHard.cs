namespace FlashCards.Models.DTOs.ToServer
{
    public class SubscribedFlashcardMarkAsHard
    {
        public int SubscribedFlashcardId { get; set; }

        public int AccountId { get; set; }

        public bool MarkedAsHard { get; set; }
    }
}
