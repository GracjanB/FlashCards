namespace FlashCards.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
