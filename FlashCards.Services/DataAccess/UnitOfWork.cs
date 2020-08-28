using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.Repositories.Implementations;
using FlashCards.Services.UnitOfWork.Abstracts;
using System;

namespace FlashCards.Services.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlashcardsDataModel _context;
        private bool disposed = false;

        private IUserRepository _userRepository;
        private IGenericRepository<User> _userGenericRepository;

        public UnitOfWork(FlashcardsDataModel context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<User> GetUserGenericRepository()
        {
            if (_userGenericRepository == null)
                _userGenericRepository = new GenericRepository<User>(_context);

            return _userGenericRepository;
        }

        public IUserRepository GetUserRepository()
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_context);

            return _userRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
