using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.DTOs.ToServer
{
    public class LessonForUpdate
    {
        [Required(ErrorMessage = "Field is required")]
        [MinLength(4, ErrorMessage = "Minimum length for this field is 4 characters")]
        [MaxLength(64, ErrorMessage = "Maximum length for this field is 64 characters")]
        public string Name { get; set; }

        [StringLength(1024, ErrorMessage = "Description characters cannot be greater than 1024")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(4, ErrorMessage = "Minimum length for this field is 4 characters")]
        [MaxLength(64, ErrorMessage = "Maximum length for this field is 64 characters")]
        public string Category { get; set; }
    }
}