﻿using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.DTOs.ToServer
{
    public class CourseForCreate
    {
        [Required(ErrorMessage = "Field is required")]
        [StringLength(maximumLength: 40, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 40 characters")]
        public string Name { get; set; }

        [MaxLength(4000, ErrorMessage = "Description characters cannot be greater than 4000")]
        public string Description { get; set; }

        /* 
         * CourseTypeEnum
         * 0 - Public
         * 1 - Private
         * 2 - Draft
         */
        [Required(ErrorMessage = "Field is required")]
        [Range(0, 2, ErrorMessage = "Course type must be set")]
        public int CourseType { get; set; }
    }
}
