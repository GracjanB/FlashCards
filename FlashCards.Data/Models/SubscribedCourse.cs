﻿using System;
using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class SubscribedCourse
    {
        public int Id { get; set; }

        public DateTime LastActivity { get; set; }

        public decimal OverallProgress { get; set; }

        public bool IsSubscribed { get; set; }


        public int AccountId { get; set; }

        public UserInfo Account { get; set; }

        public int CourseId { get; set; }

        public ICollection<SubscribedLesson> Lessons { get; set; }
    }
}
