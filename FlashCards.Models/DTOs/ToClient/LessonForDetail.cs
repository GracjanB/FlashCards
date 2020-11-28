using System.Collections.Generic;

namespace FlashCards.Models.DTOs.ToClient
{
    public class LessonForDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string DateCreated { get; set; }

        public string DateModified { get; set; }

        public bool IsSubscribed { get; set; }

        public decimal OverallProgress { get; set; }

        public ICollection<FlashcardForList> Flashcards { get; set; }
    }
}
