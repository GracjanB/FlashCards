using FlashCards.Data.DataModel;
using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.ToClient.Learn;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
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

        private readonly FlashcardsDataModel _context;

        private readonly IUserRepository _userRepository;

        private Random RandomGen { get; set; }

        public LearnService(FlashcardsDataModel context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
            FlashcardsForLearnListTest = LoadDesignData();
            RandomGen = new Random();
        }

        public void DrawFlashcards()
        {
            //var temp = InitialDrawFlashcards(FlashcardsForLearnListTest);
            //var temp2 = SetupFlashcardsForLearn(temp);

            Console.WriteLine();
        }

        public void DrawFlashcardsForLearn(int subCourseId, int userId)
        {
            // Wyszukanie ogólnie słówek do nauki - lista FlashcardForLearn
            var flashcardsForLearn = GetFlashcardsForLearn(subCourseId, userId); 
            FlashcardsForLearnListTest = flashcardsForLearn;

            // Utworzenie fiszek do nauki - wszystkich (typu FlashcardForPresentation/Selection/Input/Blocks)
            var initialFlashcardsForLearn = InitialDrawFlashcards(flashcardsForLearn); 

            // Utworzenie dwóch list z już trenowanymi fiszkami i te z nigdy
            var alreadyTrainedFlashcards = ExcludeNeverTrainedFlashcards(initialFlashcardsForLearn, out List<object> neverTrainedFlashcards); // Dwie listy 

            // Utworzenie finalnej listy fiszek przygotowanych bezpośrednio do nauki
            var finalList = ArrangeFlashcardsForLears(neverTrainedFlashcards, alreadyTrainedFlashcards);

            Console.WriteLine();
        }

        private List<object> ArrangeFlashcardsForLears(List<object> neverTrainedFlashcards, List<object> alreadyTrainedFlashcards)
        {
            List<List<object>> finalListWithFlashcardsForLearn = new List<List<object>>();
            var alreadyTrainedFlashcardsLocal = new List<object>(alreadyTrainedFlashcards);
            var neverTrainedFlashcardsLocal = new List<object>(neverTrainedFlashcards);
            var flashcardsForPresentationList = neverTrainedFlashcardsLocal.Where(x => x.GetType() == typeof(FlashcardForLearnPresentation)).ToList();
            int parts = flashcardsForPresentationList.Count() % 2 == 0 ? flashcardsForPresentationList.Count() / 2 : (int)flashcardsForPresentationList.Count() / 2; // na ile części podzielić
            var flashcardsForPresentationPartsList = flashcardsForPresentationList.Split(parts); // Podzielone na listę po dwie fiszki lub jedną jeżeli jest nieparzyście 

            // Wstępne ułożenie fiszek (Pres-Pres-(n x Selection/Input)-Pres-Pres-...
            foreach(var flashcardsPair in flashcardsForPresentationPartsList)
            {
                finalListWithFlashcardsForLearn.Add(flashcardsPair.ToList());
                var tempListWithFlashcards = new List<object>();

                var flashcardPairTemp = new List<FlashcardForLearnPresentation>();
                foreach (var el in flashcardsPair)
                    flashcardPairTemp.Add(el as FlashcardForLearnPresentation);

                foreach(var flashcardElement in flashcardPairTemp)
                {
                    var firstFlashcardSet = neverTrainedFlashcardsLocal.Where(x =>
                    {
                        if (x.GetType() == typeof(FlashcardForLearnSelection))
                        {
                            var flashcard = x as FlashcardForLearnSelection;
                            return flashcard.FlashcardId == flashcardElement.FlashcardId;
                        }
                        if (x.GetType() == typeof(FlashcardForLearnInput))
                        {
                            var flashcard = x as FlashcardForLearnInput;
                            return flashcard.FlashcardId == flashcardElement.FlashcardId;
                        }
                        if (x.GetType() == typeof(FlashcardForLearnBlocks))
                        {
                            var flashcard = x as FlashcardForLearnBlocks;
                            return flashcard.FlashcardId == flashcardElement.FlashcardId;
                        }
                        else return false;
                    }).ToList();

                    // Dodanie fiszek do nauki do tymczasowej listy
                    var indexForDivideToHalf = (int)firstFlashcardSet.Count() / 2;
                    var firstFlashcardSetHalf = firstFlashcardSet.GetRange(0, indexForDivideToHalf);
                    tempListWithFlashcards = tempListWithFlashcards.Append(firstFlashcardSetHalf).ToList();

                    // Usunięcie tych elementów z lokalnej listy
                    foreach (var element in firstFlashcardSetHalf)
                        neverTrainedFlashcardsLocal.Remove(element);
                }

                finalListWithFlashcardsForLearn.Add(tempListWithFlashcards.ToList());
            }

            var flashcardsThatCanBeAdded = new List<FlashcardForLearnPresentation>();

            // Dopełnienie fiszkami 
            for(int i = 0; i < finalListWithFlashcardsForLearn.Count(); i++)
            {
                // Jeżeli jest to zbiór fiszek prezentujących słowo
                if(finalListWithFlashcardsForLearn[i][1].GetType() == typeof(FlashcardForLearnPresentation))
                {
                    var flashcardsToAdd = new List<object>();

                    var flashcardsToSearch = new List<object>(flashcardsThatCanBeAdded);
                    foreach (var ell in finalListWithFlashcardsForLearn[i])
                        flashcardsToSearch.Add(ell as FlashcardForLearnPresentation);

                    foreach(var element in flashcardsToSearch)
                    {
                        var firstFlashcardSet = neverTrainedFlashcardsLocal.Where(x =>
                        {
                            if (x.GetType() == typeof(FlashcardForLearnSelection))
                            {
                                var flashcard = x as FlashcardForLearnSelection;
                                var flashcardToFind = element as FlashcardForLearnPresentation;
                                return flashcard.FlashcardId == flashcardToFind.FlashcardId;
                            }
                            if (x.GetType() == typeof(FlashcardForLearnInput))
                            {
                                var flashcard = x as FlashcardForLearnInput;
                                var flashcardToFind = element as FlashcardForLearnPresentation;
                                return flashcard.FlashcardId == flashcardToFind.FlashcardId;
                            }
                            if (x.GetType() == typeof(FlashcardForLearnBlocks))
                            {
                                var flashcard = x as FlashcardForLearnBlocks;
                                var flashcardToFind = element as FlashcardForLearnPresentation;
                                return flashcard.FlashcardId == flashcardToFind.FlashcardId;
                            }
                            else return false;
                        }).ToList();

                        var indexForDivideToHalf = (int)((firstFlashcardSet.Count() + 0.5) / 2);

                        for (int x = 0; x < 100; x++)
                            firstFlashcardSet.Shuffle();

                        var firstFlashcardSetHalf = firstFlashcardSet.GetRange(0, indexForDivideToHalf);

                        foreach (var el in firstFlashcardSetHalf)
                        {
                            flashcardsToAdd.Add(el);
                            neverTrainedFlashcardsLocal.Remove(el); // Usunięcie wybranych już fiszek z głównej listy
                        }

                        flashcardsThatCanBeAdded.Add(element as FlashcardForLearnPresentation);
                    }

                    var temp = finalListWithFlashcardsForLearn[i + 1];
                    foreach (var el in flashcardsToAdd)
                        temp.Add(el);

                    for (int j = 0; j < 100; j++)
                        temp.Shuffle();

                    finalListWithFlashcardsForLearn[i + 1] = temp;
                }
            }

            var indexOfHalfAlreadyTrainedFlashcards = (int)alreadyTrainedFlashcards.Count() / 2;
            var alreadyTrainedFlashcardsListHalf = alreadyTrainedFlashcardsLocal.GetRange(0, indexOfHalfAlreadyTrainedFlashcards);

            for (int x = 0; x < 100; x++)
                alreadyTrainedFlashcardsListHalf.Shuffle();

            var finalListWithFlashcardsTemp = new List<List<object>>();
            finalListWithFlashcardsTemp.Add(alreadyTrainedFlashcardsListHalf);

            foreach (var element in finalListWithFlashcardsForLearn)
                finalListWithFlashcardsTemp.Add(element);

            finalListWithFlashcardsForLearn = finalListWithFlashcardsTemp;

            // Wyrzucenie tych fiszek, które zostały dodane do głównej listy
            foreach (var element in alreadyTrainedFlashcardsListHalf)
                alreadyTrainedFlashcardsLocal.Remove(element);

            // Zrobienie ogólnej listy z pozostałymi fiszkami
            var remainedFlashcards = new List<object>();
            foreach (var element in alreadyTrainedFlashcardsLocal)
                remainedFlashcards.Add(element);
            foreach (var element in neverTrainedFlashcardsLocal)
                if (element.GetType() != typeof(FlashcardForLearnPresentation))
                    remainedFlashcards.Add(element);

            // W następnym kroku przeliczyć ile jest wystąpień par typu FlashcardForLearnPresentation
            // i podzielić lisę już trenowanych fiszek na tyle części do ilu można je wstawić pomiędzy tymi parami
            // Potem zrobić podobną pętle jak wyżej i do każdego segmentu dorzucić te fiszki, potasować i dorzucić do zbioru
            // Jeżeli coś pozostało z listy fiszek nauczonych to przerzucić to do następnego elementu na tamtej liście
            // I tak do końca. Jeżeli pętla będzie na samym końcu i coś pozostanie ogólnie to wrzucić tam już wszystko wczesniej przetasowane


            Console.WriteLine();


            throw new NotImplementedException();
        }

        private List<FlashcardForLearn> GetFlashcardsForLearn(int subCourseId, int userId)
        {
            var remainedFlashcardsForLearn = _userRepository.GetDetail(userId).UserInfo.NumberOfWordsInLearningSession;
            var subFlashcardsIds = new List<int>();

            var subLessons = _context.SubscribedLessons
                .Include(x => x.SubscribedFlashcards)
                .Where(x => x.SubscribedCourseId == subCourseId)
                .OrderBy(x => x.LastTrainingDate)
                .ToList();

            foreach(var subLesson in subLessons)
            {
                if (remainedFlashcardsForLearn == 0)
                    break;

                subLesson.SubscribedFlashcards = subLesson.SubscribedFlashcards.OrderByDescending(x => x.TrainLevel).ToList();

                foreach(var flashcard in subLesson.SubscribedFlashcards)
                {
                    if (remainedFlashcardsForLearn != 0)
                    {
                        if (flashcard.TrainLevel < 10 && !flashcard.MarkedAsIgnored)
                        {
                            subFlashcardsIds.Add(flashcard.Id);
                            remainedFlashcardsForLearn--;
                        }
                    }
                    else break;
                }
            }

            var flashcardToLearn = _context.SubscribedFlashcards
                .Join(_context.Flashcards.Where(x => subFlashcardsIds.Contains(x.Id)),
                    subscribedFlashcard => subscribedFlashcard.Id,
                    flashcard => flashcard.Id,
                    (subscribedFlashcard, flashcard) => new FlashcardForLearn
                    {
                        FlashcardId = flashcard.Id,
                        FlashcardSubscriptionId = subscribedFlashcard.Id,
                        Phrase = flashcard.Phrase,
                        PhrasePronunciation = flashcard.PhrasePronunciation,
                        PhraseSampleSentence = flashcard.PhraseSampleSentence,
                        PhraseComment = flashcard.PhraseComment,
                        TranslatedPhrase = flashcard.TranslatedPhrase,
                        TranslatedPhraseSampleSentence = flashcard.TranslatedPhraseSampleSentence,
                        TranslatedPhraseComment = flashcard.TranslatedPhraseComment,
                        LanguageLevel = flashcard.LanguageLevel.GetDescription(),
                        TrainLevel = subscribedFlashcard.TrainLevel,
                        MarkedAsHard = subscribedFlashcard.MarkedAsHard,
                        LastTrainingDate = subscribedFlashcard.LastTrainingDate
                    }).ToList();

            return flashcardToLearn;
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
                        flashcardsForReturn.Add(CreateFlashcardForLearnSelection(flashcardForLearn));
                        remainedSum -= 1;
                    }
                    else
                    {
                        int randomNumber = RandomGen.Next(0, 100);

                        if (randomNumber < 50)
                        {
                            flashcardsForReturn.Add(CreateFlashcardForLearnSelection(flashcardForLearn));
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
