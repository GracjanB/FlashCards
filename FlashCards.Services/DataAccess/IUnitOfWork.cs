using System;

namespace FlashCards.Services.UnitOfWork.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        void SaveAsync();
    }
}
