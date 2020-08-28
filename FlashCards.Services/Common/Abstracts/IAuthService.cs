using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;

namespace FlashCards.Services.Abstracts
{
    public interface IAuthService
    {
        TokenDTO Login(string email, string password);

        bool Logout(int userId);

        bool Register(User user, string password);

        bool AuthorizeUser(int userId, string token);
    }
}
