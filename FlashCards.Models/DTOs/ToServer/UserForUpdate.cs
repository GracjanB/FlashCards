using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.DTOs.ToServer
{
    public class UserForUpdate
    {
        [MaxLength(64, ErrorMessage = "Field cannot be longer than 64 characters")]
        public string FirstName { get; set; }

        [MaxLength(64, ErrorMessage = "Field cannot be longer than 64 characters")]
        public string LastName { get; set; }

        [MaxLength(32, ErrorMessage = "Field cannot be longer than 32 characters")]
        public string DisplayName { get; set; }

        [MaxLength(64, ErrorMessage = "Field cannot be longer than 64 characters")]
        public string City { get; set; }

        [MaxLength(64, ErrorMessage = "Field cannot be longer than 64 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int NumberOfWordsInLearningSession { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int NumberOfWordsInReviewSession { get; set; }
    }
}
