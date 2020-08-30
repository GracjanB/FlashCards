using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.DTOs.ToServer
{
    public class UserForPasswordChange
    {
        [Required(ErrorMessage = "Field is required")]
        [StringLength(maximumLength: 32, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 32")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(maximumLength: 32, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 32")]
        public string NewPassword { get; set; }
    }
}
