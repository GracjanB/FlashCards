using System;
using System.Collections;
using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }


        public int AccountCreatedId { get; set; }

        public UserInfo AccountCreated { get; set; }

        public CourseInfo CourseInfo { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<CourseOpinion> Opinions { get; set; }
    }
}
