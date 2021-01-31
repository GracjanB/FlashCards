export class FlashcardForLearn {
  flashcardId: number;
  flashcardSubscriptionId: number;
  phrase: string;
  phrasePronunciation: string;
  phraseSampleSentence: string;
  phraseComment: string;
  translatedPhrase: string;
  translatedPhraseSampleSentence: string;
  translatedPhraseComment: string;
  languageLevel: string;
  category: string;
  trainLevel: number;
  markedAsHard: boolean;
  lastTrainingDate: Date;

  public initialize(flashcardId: number,
                    flashcardSubscriptionId: number,
                    phrase: string,
                    phrasePronunciation: string,
                    phraseSampleSentence: string,
                    phraseComment: string,
                    translatedPhrase: string,
                    translatedPhraseSampleSentence: string,
                    translatedPhraseComment: string,
                    languageLevel: string,
                    category: string,
                    trainLevel: number,
                    markedAsHard: boolean,
                    lastTrainingDate: Date): void {
    this.flashcardId = flashcardId;
    this.flashcardSubscriptionId = flashcardSubscriptionId;
    this.phrase = phrase;
    this.phrasePronunciation = phrasePronunciation;
    this.phraseSampleSentence = phraseSampleSentence;
    this.phraseComment = phraseComment;
    this.translatedPhrase = translatedPhrase;
    this.translatedPhraseSampleSentence = translatedPhraseSampleSentence;
    this.translatedPhraseComment = translatedPhraseComment;
    this.languageLevel = languageLevel;
    this.category = category;
    this.trainLevel = trainLevel;
    this.markedAsHard = markedAsHard;
    this.lastTrainingDate = lastTrainingDate;
  }

  public initializeWithFlashcard(flashcard: FlashcardForLearn): void {
    this.flashcardId = flashcard.flashcardId;
    this.flashcardSubscriptionId = flashcard.flashcardSubscriptionId;
    this.phrase = flashcard.phrase;
    this.phrasePronunciation = flashcard.phrasePronunciation;
    this.phraseSampleSentence = flashcard.phraseSampleSentence;
    this.phraseComment = flashcard.phraseComment;
    this.translatedPhrase = flashcard.translatedPhrase;
    this.translatedPhraseSampleSentence = flashcard.translatedPhraseSampleSentence;
    this.translatedPhraseComment = flashcard.translatedPhraseComment;
    this.languageLevel = flashcard.languageLevel;
    this.category = flashcard.category;
    this.trainLevel = flashcard.trainLevel;
    this.markedAsHard = flashcard.markedAsHard;
    this.lastTrainingDate = flashcard.lastTrainingDate;
  }
}
