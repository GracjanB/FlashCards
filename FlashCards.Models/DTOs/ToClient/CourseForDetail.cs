using System.Collections.Generic;

namespace FlashCards.Models.DTOs.ToClient
{
    public class CourseForDetail
    {
        public int Id { get; set; }

        public int AccountCreatedId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CourseType { get; set; }

        public string DateCreated { get; set; }

        public string DateModified { get; set; }

        public string AmountOfEnrolled { get; set; }

        public bool IsSubscribing { get; set; }

        public ICollection<LessonForList> Lessons { get; set; }
    }
}
