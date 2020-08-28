using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.DTOs.ToServer
{
    public class UserForLogin
    {
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }
    }
}
