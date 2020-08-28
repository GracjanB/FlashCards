namespace FlashCards.Models.DTOs.ToServer
{
    public class UserForRegister
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
