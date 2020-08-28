using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.UnitOfWork.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace FlashCards.Services.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FlashcardsDataModel _context;

        public UserRepository(FlashcardsDataModel context) : base(context)
        {
            _context = context;
        }

        public User GetDetail(int id)
        {
            throw new NotImplementedException();
        }

        public User GetDetail(string email)
        {
            return _context.Users.Include(x => x.UserInfo).FirstOrDefault(x => x.Email == email);
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
    }
}
