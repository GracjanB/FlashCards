using FlashCards.Models.DTOs.ToClient.Learn;
using System.Collections.Generic;

namespace FlashCards.Services.Common.Abstracts
{
    public interface ILearnService
    {
        LearnConfiguration DrawFlashcardsForLearn(int subCourseId, int userId);

        LearnConfiguration DrawFlashcardsForLearn(int subCourseId, int subLessonId, int userId);

        RepetitionConfiguration DrawFlashcardsForRepetition(int subCourseId, int userId);

        RepetitionConfiguration DrawFlashcardsForRepetition(int subCourseId, int userId, int subLessonId);

        HardWordsLearnConfiguration DrawFlashcardsForHardWordsLearning(int subLessonId);

        bool SaveLearnResult(List<FlashcardForLearn> flashcardsForLearn);

        bool SaveRepetitionResult(List<FlashcardForLearn> flashcardsForLearn);
    }
}
