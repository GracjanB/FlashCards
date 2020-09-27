using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.UnitOfWork.Implementations;

namespace FlashCards.Services.Repositories.Implementations
{
    public class SubscribedCourseRepository : ISubscribedCourseRepository
    {
        private readonly FlashcardsDataModel _context;

        public SubscribedCourseRepository(FlashcardsDataModel context)
        {
            _context = context;
        }
    }
}
