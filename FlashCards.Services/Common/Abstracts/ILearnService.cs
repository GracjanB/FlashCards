namespace FlashCards.Services.Common.Abstracts
{
    public interface ILearnService
    {
        void DrawFlashcards();

        void DrawFlashcardsForLearn(int subCourseId, int userId);
    }
}
