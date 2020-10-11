using System;
using System.Collections;
using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }


        public int CourseId { get; set; }

        public Course Course { get; set; }

        public ICollection<Flashcard> Flashcards { get; set; }
    }
}
