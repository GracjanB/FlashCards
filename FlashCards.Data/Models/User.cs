using FlashCards.Data.Enums;

namespace FlashCards.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public UserRoleEnum Role { get; set; }

        public int UserInfoId { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
