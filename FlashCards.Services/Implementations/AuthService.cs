using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Abstracts;
using System;

namespace FlashCards.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public TokenDTO Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool Logout(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Register(UserForRegister userForRegister)
        {
            throw new NotImplementedException();
        }

        public bool AuthorizeUser(int userId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
