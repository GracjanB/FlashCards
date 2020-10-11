using FlashCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface IFlashcardRepository
    {
        Task<List<Flashcard>> Get(int lessonId);

        Task<bool> Create(int lessonId, Flashcard flashcard);

        Task<bool> Create(int lessonId, IEnumerable<Flashcard> flashcards);

        Task<bool> Update(Flashcard flashcard);
    }
}
