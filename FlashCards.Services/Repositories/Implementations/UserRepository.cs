using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FlashcardsDataModel _context;

        public UserRepository(FlashcardsDataModel context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public User GetDetail(int id)
        {
            return _context.Users.Include(x => x.UserInfo).FirstOrDefault(x => x.Id == id);
        }

        public User GetDetail(string email)
        {
            return _context.Users.Include(x => x.UserInfo).FirstOrDefault(x => x.Email == email);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include(x => x.UserInfo).ToList();
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public int GetUserAccountId(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            return user != null ? user.UserInfoId : 0;
        }

        
    }
}
