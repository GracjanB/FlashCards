using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface IUserRepository 
    {
        User Get(int id);

        UserForDetailCourses GetDetailedWithCourses(int id);

        Task Create(User user);

        Task Update(User user);

        User Update(int userId, UserForUpdate userForUpdate);

        bool UserExists(string email);

        User GetDetail(int id);

        User GetDetail(string email);

        List<User> GetAllUsers();

        int GetUserAccountId(int userId);

        bool IsAdministrator(int userId);
    }
}
