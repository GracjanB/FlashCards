using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Services.Common.Abstracts
{
    public interface ISubscribedCourseManagerService
    {
        bool MarkFlashcardAsHard(int subscribedFlashcardId, int accountId, bool isHard);

        bool MarkFlashcardAsIgnored(int subscribedFlashcardId, int accountId, bool isIgnored);

        bool ClearCourseProgress(int subscribedCourseId, int accountId);

        bool ClearLessonProgress(int subscribedLessonId, int accountId);

        bool ClearFlashcardProgress(int subscribedFlashcardId, int accountId);
    }
}
