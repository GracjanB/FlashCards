using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient
{
    public class CourseForList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DateCreated { get; set; }

        public string NumberOfEnrolled { get; set; }

        public string AuthorDisplayName { get; set; }

        public int NumberOfRatings { get; set; }

        public decimal AverageRating { get; set; }
    }
}
