using FlashCards.Data.Models;
using FlashCards.Services.Repositories.Abstracts;
using System;

namespace FlashCards.Services.UnitOfWork.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> GetUserGenericRepository();

        IUserRepository GetUserRepository();

        void Save();

        void SaveAsync();
    }
}
