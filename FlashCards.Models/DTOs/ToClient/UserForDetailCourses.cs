using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient
{
    public class UserForDetailCourses : UserForDetail
    {
        public IEnumerable<CourseForList> CreatedCourses { get; set; }

        public IEnumerable<CourseForList> SubscribedCourses { get; set; }

        public IEnumerable<CourseForList> PrivateCourses { get; set; }

        public IEnumerable<CourseForList> DraftCourses { get; set; }

        public int NumberOfCreatedCourses { get; set; }

        public int NumberOfSubscribedCourses { get; set; }

        public int NumberOfAlreadyLearntFlashcards { get; set; }
    }
}
