using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Services.Abstracts
{
    public interface IAuthService
    {
        TokenDTO Login(string email, string password);

        bool Register(User user, string password);

        void CreateNewPassword(string newPassword, ref User user);

        bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);

        bool RegisterAdministrator(User user, string password);
    }
}
