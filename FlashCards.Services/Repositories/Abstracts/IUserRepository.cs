using FlashCards.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface IUserRepository 
    {
        User Get(int id);

        Task Create(User user);

        Task Update(User user);

        bool UserExists(string email);

        User GetDetail(int id);

        User GetDetail(string email);

        List<User> GetAllUsers();

        int GetUserAccountId(int userId);
    }
}
