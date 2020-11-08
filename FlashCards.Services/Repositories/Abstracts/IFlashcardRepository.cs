using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface IFlashcardRepository
    {
        Task<List<Flashcard>> Get(int lessonId);

        Task<Flashcard> GetFlashcard(int id);

        Task<Flashcard> Create(int lessonId, Flashcard flashcard);

        Task<bool> Create(int lessonId, IEnumerable<Flashcard> flashcards);

        Task<Flashcard> Update(int id, FlashcardForUpdate flashcard);
    }
}
