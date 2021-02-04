using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.ToClient.Learn;
using FlashCards.Services.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashCards.Services.Common.Implementations
{
    public class LearnService : ILearnService
    {
        private const int AmountOfTrainLevel = 5;

        private const int PointsForInput = 2;

        private const int PointsForSelection = 1;

        private const int PointsForPresentation = 1;

        private const int PointsForBlocks = 1;

        private List<object> FlashcardsForLearnList { get; set; }

        private List<FlashcardForLearn> FlashcardsForLearnListTest { get; set; }

        private Random RandomGen { get; set; }

        public LearnService()
        {
            FlashcardsForLearnListTest = LoadDesignData();
            RandomGen = new Random();
        }

        public void DrawFlashcards()
        {
            var temp = InitialDrawFlashcards(FlashcardsForLearnListTest);
            var temp2 = SetupFlashcardsForLearn(temp);

            Console.WriteLine();
        }

        private List<object> InitialDrawFlashcards(List<FlashcardForLearn> flashcardForLearnList)
        {
            List<object> flashcardsForReturn = new List<object>();

            foreach (var flashcardForLearn in flashcardForLearnList)
            {
                int remainedSum = AmountOfTrainLevel;

                if (flashcardForLearn.TrainLevel == 0)
                {
                    flashcardsForReturn.Add(new FlashcardForLearnPresentation(flashcardForLearn));
                    remainedSum -= PointsForPresentation;
                }

                if (flashcardForLearn.TrainLevel >= 6)
                {
                    flashcardsForReturn.Add(new FlashcardForLearnInput(flashcardForLearn));
                    continue;
                }

                while (remainedSum > 0)
                {
                    if (remainedSum == 1)
                    {
                        flashcardsForReturn.Add(new FlashcardForLearnSelection(flashcardForLearn));
                        remainedSum -= 1;
                    }
                    else
                    {
                        int randomNumber = RandomGen.Next(0, 100);

                        if (randomNumber < 50)
                        {
                            flashcardsForReturn.Add(new FlashcardForLearnSelection(flashcardForLearn));
                            remainedSum -= PointsForSelection;
                        }
                        else
                        {
                            flashcardsForReturn.Add(new FlashcardForLearnInput(flashcardForLearn));
                            remainedSum -= PointsForInput;
                        }
                    }
                }
            }

            return flashcardsForReturn;
        }

        private List<object> SetupFlashcardsForLearn(List<object> flashcards)
        {
            // Przemyśleć przypadek jeżeli nie ma nigdy nie trenowanych fiszek
            // Przemyśleć przypadek jeżeli nie ma już trenowanych fiszek

            List<object> flashcardToReturn = new List<object>();
            var flashcardsForLearnAlreadyTrainedList = ExcludeNeverTrainedFlashcards(flashcards, out List<object> excludedFlashcardsList);
            var excludedFlashcards = excludedFlashcardsList;
            var amountOfFlashcardToInitialLearn = (int)flashcardsForLearnAlreadyTrainedList.Count / 2;

            for (int i = 0; i <= 5; i++)
                flashcardsForLearnAlreadyTrainedList.Shuffle();

            if (amountOfFlashcardToInitialLearn > 5)
            {
                flashcardToReturn = flashcardsForLearnAlreadyTrainedList.GetRange(0, 5);
                flashcardsForLearnAlreadyTrainedList.RemoveRange(0, 5);
            }
            else
            {
                flashcardToReturn = flashcardsForLearnAlreadyTrainedList.GetRange(0, amountOfFlashcardToInitialLearn);
                flashcardsForLearnAlreadyTrainedList.RemoveRange(0, amountOfFlashcardToInitialLearn);
            }

            Console.WriteLine();

            return flashcardToReturn;
        }

        /// <summary>
        /// Function excludes never trained flashcards (based on where is type FlashcardForLearnPresentation)
        /// and other related to flashcard objects (based on FlashcardId)
        /// </summary>
        /// <param name="flashcards">List of flashcard objects from initial draw</param>
        /// <param name="excludedFlashcards">List of never trained flashcard objects</param>
        /// <returns>List of already trained flashcard objects (train level > 0)</returns>
        private List<object> ExcludeNeverTrainedFlashcards(List<object> flashcards, out List<object> excludedFlashcards)
        {
            List<object> flashcardsForLearnList = null;
            List<object> excludedFlashcardsList = null;

            var excludedFlashcardIds = flashcards
                .Where(x => x.GetType() == typeof(FlashcardForLearnPresentation))
                .Select(x =>
                {
                    var flashcardPresentation = x as FlashcardForLearnPresentation;
                    return flashcardPresentation.FlashcardId;
                }).ToList();

            excludedFlashcardsList = flashcards.Where(x =>
            {
                if (x.GetType() == typeof(FlashcardForLearnSelection))
                {
                    var flashcard = x as FlashcardForLearnSelection;
                    return excludedFlashcardIds.Contains(flashcard.FlashcardId);
                }
                if (x.GetType() == typeof(FlashcardForLearnPresentation))
                {
                    var flashcard = x as FlashcardForLearnPresentation;
                    return excludedFlashcardIds.Contains(flashcard.FlashcardId);
                }
                if (x.GetType() == typeof(FlashcardForLearnInput))
                {
                    var flashcard = x as FlashcardForLearnInput;
                    return excludedFlashcardIds.Contains(flashcard.FlashcardId);
                }
                if (x.GetType() == typeof(FlashcardForLearnBlocks))
                {
                    var flashcard = x as FlashcardForLearnBlocks;
                    return excludedFlashcardIds.Contains(flashcard.FlashcardId);
                }
                else return false;
            }).ToList();

            excludedFlashcards = excludedFlashcardsList;
            flashcardsForLearnList = flashcards.Where(x => !excludedFlashcardsList.Contains(x)).ToList();

            return flashcardsForLearnList;
        }

        private FlashcardForLearnSelection CreateFlashcardForLearnSelection(FlashcardForLearn flashcardForLearn)
        {
            var flashcardForSelection = new FlashcardForLearnSelection(flashcardForLearn);
            var wordsForSelection = new List<string>();

            foreach (var flashcard in FlashcardsForLearnListTest)
            {
                if (flashcard == flashcardForLearn)
                    continue;

                wordsForSelection.Add(flashcard.TranslatedPhrase);
            }

            for (int i = 0; i < 5; i++)
                wordsForSelection.Shuffle();

            wordsForSelection = wordsForSelection.GetRange(0, 3);
            wordsForSelection.Add(flashcardForLearn.TranslatedPhrase);

            for (int i = 0; i < 5; i++)
                wordsForSelection.Shuffle();

            flashcardForSelection.FlashcardsForSelection = wordsForSelection;

            return flashcardForSelection;
        }



        private List<FlashcardForLearn> LoadDesignData()
        {
            List<FlashcardForLearn> flashcardsForLearnList = new List<FlashcardForLearn>();
            flashcardsForLearnList.Add(new FlashcardForLearn
            {
                FlashcardId = 1,
                FlashcardSubscriptionId = 1,
                Phrase = "Addictive",
                PhrasePronunciation = "adiktiw",
                PhraseSampleSentence = "I am so addictive",
                PhraseComment = "Comment",
                TranslatedPhrase = "Uzależniający",
                TranslatedPhraseSampleSentence = "Jestem bardzo uzależniający",
                TranslatedPhraseComment = "Komentarz",
                LanguageLevel = "B1",
                TrainLevel = 3,
                MarkedAsHard = true,
                LastTrainingDate = new DateTime(2020, 11, 13)
            });
            flashcardsForLearnList.Add(new FlashcardForLearn
            {
                FlashcardId = 3,
                FlashcardSubscriptionId = 14,
                Phrase = "to look for",
                PhrasePronunciation = "tu luk for",
                PhraseSampleSentence = "I am looking for a job",
                PhraseComment = "Comment",
                TranslatedPhrase = "szukać",
                TranslatedPhraseSampleSentence = "Szukam pracy",
                TranslatedPhraseComment = "Komentarz",
                LanguageLevel = "B2",
                TrainLevel = 0,
                MarkedAsHard = false,
                LastTrainingDate = new DateTime(2020, 12, 4)
            });
            flashcardsForLearnList.Add(new FlashcardForLearn
            {
                FlashcardId = 65,
                FlashcardSubscriptionId = 456,
                Phrase = "go after",
                PhrasePronunciation = "goł after",
                PhraseSampleSentence = "Police is going after a robber",
                PhraseComment = "Comment",
                TranslatedPhrase = "ścigać",
                TranslatedPhraseSampleSentence = "Policja ściga włamywacza",
                TranslatedPhraseComment = "Komentarz",
                LanguageLevel = "B2",
                TrainLevel = 7,
                MarkedAsHard = false,
                LastTrainingDate = new DateTime(2020, 11, 14)
            });
            flashcardsForLearnList.Add(new FlashcardForLearn
            {
                FlashcardId = 345,
                FlashcardSubscriptionId = 578,
                Phrase = "a meeting",
                PhrasePronunciation = "ae meeting",
                PhraseSampleSentence = "A meeting with friends",
                PhraseComment = "Comment",
                TranslatedPhrase = "spotkanie",
                TranslatedPhraseSampleSentence = "Spotkanie z przyjaciółmi",
                TranslatedPhraseComment = "Komentarz",
                LanguageLevel = "B2",
                TrainLevel = 4,
                MarkedAsHard = true,
                LastTrainingDate = new DateTime(2020, 12, 5)
            });
            flashcardsForLearnList.Add(new FlashcardForLearn
            {
                FlashcardId = 735,
                FlashcardSubscriptionId = 1234,
                Phrase = "simplify",
                PhrasePronunciation = "simplifaj",
                PhraseSampleSentence = "I would like to simplify the task",
                PhraseComment = "Comment",
                TranslatedPhrase = "ułatwiać",
                TranslatedPhraseSampleSentence = "Chciałbym ułatwić to zadanie",
                TranslatedPhraseComment = "Komentarz",
                LanguageLevel = "B1",
                TrainLevel = 1,
                MarkedAsHard = false,
                LastTrainingDate = new DateTime(2020, 11, 13)
            });
            return flashcardsForLearnList;
        }
    }
}
