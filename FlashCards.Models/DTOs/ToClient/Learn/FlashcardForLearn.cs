using System;

namespace FlashCards.Models.DTOs.ToClient.Learn
{
    public class FlashcardForLearn
    {
        public int FlashcardId { get; set; }

        public int FlashcardSubscriptionId { get; set; }

        public string Phrase { get; set; }

        public string PhrasePronunciation { get; set; }

        public string PhraseSampleSentence { get; set; }

        public string PhraseComment { get; set; }

        public string TranslatedPhrase { get; set; }

        public string TranslatedPhraseSampleSentence { get; set; }

        public string TranslatedPhraseComment { get; set; }

        public string LanguageLevel { get; set; }

        public int TrainLevel { get; set; }

        public bool MarkedAsHard { get; set; }

        public DateTime LastTrainingDate { get; set; }


        public FlashcardForLearn() { }

        public FlashcardForLearn(FlashcardForLearn flashcardForLearn)
        {
            FlashcardId = flashcardForLearn.FlashcardId;
            FlashcardSubscriptionId = flashcardForLearn.FlashcardSubscriptionId;
            Phrase = flashcardForLearn.Phrase;
            PhrasePronunciation = flashcardForLearn.PhrasePronunciation;
            PhraseSampleSentence = flashcardForLearn.PhraseSampleSentence;
            PhraseComment = flashcardForLearn.PhraseComment;
            TranslatedPhrase = flashcardForLearn.TranslatedPhrase;
            TranslatedPhraseSampleSentence = flashcardForLearn.TranslatedPhraseSampleSentence;
            TranslatedPhraseComment = flashcardForLearn.TranslatedPhraseComment;
            LanguageLevel = flashcardForLearn.LanguageLevel;
            TrainLevel = flashcardForLearn.TrainLevel;
            MarkedAsHard = flashcardForLearn.MarkedAsHard;
            LastTrainingDate = flashcardForLearn.LastTrainingDate;
        }
    }
}
