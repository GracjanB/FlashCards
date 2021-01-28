using System;
using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string PhotoUrl { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime DateCreated { get; set; }

        public int NumberOfWordsInLearningSession { get; set; }

        public int NumberOfWordsInReviewSession { get; set; }


        public User User { get; set; }

        public ICollection<Course> CreatedCourses { get; set; }

        public ICollection<SubscribedCourse> SubscribedCourses { get; set; }
    }
}
