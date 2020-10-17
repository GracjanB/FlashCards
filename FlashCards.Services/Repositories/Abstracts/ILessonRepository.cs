using FlashCards.Data.Models;
using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface ILessonRepository
    {
        Task<Lesson> Get(int id);

        Task<PagedList<Lesson>> GetLessons(int courseId, LessonParams lessonParams);

        Task<bool> Create(int courseId, Lesson lesson);

        Task<bool> Create(int courseId, IEnumerable<Lesson> lessons);

        Task<bool> Update(int lessonId, LessonForUpdate lessonForUpdate);
    }
}
