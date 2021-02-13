using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient
{
    public class LessonForCheck : LessonForList
    {
        public List<FlashcardForDetail> Flashcards { get; set; }
    }
}
