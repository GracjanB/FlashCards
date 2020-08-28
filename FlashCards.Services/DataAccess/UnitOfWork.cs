using FlashCards.Data.DataModel;
using FlashCards.Services.UnitOfWork.Abstracts;
using System;

namespace FlashCards.Services.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private FlashcardsDataModel _context;
        private bool disposed = false;

        public UnitOfWork(FlashcardsDataModel context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context));
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
            if(!this.disposed)
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
