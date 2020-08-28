using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;

namespace FlashCards.Services.Abstracts
{
    public interface IAuthService
    {
        TokenDTO Login(string email, string password);

        bool Logout(int userId);

        bool Register(UserForRegister userForRegister);

        bool AuthorizeUser(int userId, string token);
    }
}
