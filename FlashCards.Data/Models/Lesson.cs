using System;

namespace FlashCards.Data.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
