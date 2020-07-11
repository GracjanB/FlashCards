using System;

namespace FlashCards.Data.Models
{
    public class CourseOpinion
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }


        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
