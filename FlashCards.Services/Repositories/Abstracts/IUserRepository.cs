using FlashCards.Data.Models;
using FlashCards.Services.UnitOfWork.Abstracts;
using System.Collections.Generic;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool UserExists(string email);

        User GetDetail(int id);

        User GetDetail(string email);

        List<User> GetAllUsers();
    }
}
