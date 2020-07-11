using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }


        public User User { get; set; }

        public ICollection<Course> CreatedCourses { get; set; }

        public ICollection<UserCourse> CoursesEnrolled { get; set; }
    }
}
