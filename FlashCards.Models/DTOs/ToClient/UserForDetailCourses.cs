using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient
{
    public class UserForDetailCourses : UserForDetail
    {
        public IEnumerable<CourseShort> CreatedCourses { get; set; }

        public IEnumerable<CourseShort> SubscribedCourses { get; set; }
    }
}
