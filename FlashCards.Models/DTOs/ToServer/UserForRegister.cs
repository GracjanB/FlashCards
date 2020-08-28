using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.DTOs.ToServer
{
    public class UserForRegister
    {
        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [MaxLength(ErrorMessage = "Maximum length can't be greater than 128")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(maximumLength: 32, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 32")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(maximumLength: 32, MinimumLength = 4, ErrorMessage = "DisplayName must be between 4 and 32")]
        public string DisplayName { get; set; }

        [StringLength(maximumLength: 32, ErrorMessage = "FirstName characters cannot be greater than 32")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 32, ErrorMessage = "LastName characters cannot be greater than 32")]
        public string LastName { get; set; }

        [StringLength(maximumLength: 64, ErrorMessage = "City characters cannot be greater than 64")]
        public string City { get; set; }

        [StringLength(maximumLength: 32, ErrorMessage = "Country characters cannot be greater than 32")]
        public string Country { get; set; }
    }
}
