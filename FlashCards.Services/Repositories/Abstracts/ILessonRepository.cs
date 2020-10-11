using FlashCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface ILessonRepository
    {
        Task<Lesson> Get(int id);

        Task<List<Lesson>> GetLessons(int courseId);

        Task<bool> Create(int courseId, Lesson lesson);

        Task<bool> Create(int courseId, IEnumerable<Lesson> lessons);

        Task<bool> Update(Lesson lesson);
    }
}
