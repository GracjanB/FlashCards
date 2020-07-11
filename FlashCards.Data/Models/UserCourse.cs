using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class UserCourse
    {
        public int Id { get; set; }


        public int AccountId { get; set; }

        public Account Account { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public ICollection<UserLesson> Lessons { get; set; }
    }
}
