namespace FlashCards.Models.DTOs.ToServer
{
    public class SubscribedFlashcardMarkAsIgnored
    {
        public int SubscribedFlashcardId { get; set; }

        public int AccountId { get; set; }

        public bool MarkAsIgnored { get; set; }
    }
}
