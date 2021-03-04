import {Injectable} from '@angular/core';
import {FlashcardForLearn} from '../_models/_dtos/fromServer/flashcardForLearn';
import {FlashcardLearnForPresentation} from '../_models/flashcardLearnForPresentation';
import {FlashcardLearnForInput} from '../_models/flashcardLearnForInput';
import {FlashcardLearnForSelection} from '../_models/flashcardLearnForSelection';
import {LearnConfiguration} from '../_models/learnConfiguration';
import {LearnTypeEnum} from '../_models/enums/learnTypeEnum';

@Injectable({
  providedIn: 'root'
})
export class LearnAdapter {

  adaptLearnConfiguration(element: any): LearnConfiguration {
    const learnConfiguration = new LearnConfiguration();
    // Adapt flashcards that were drawn to learn
    learnConfiguration.drawnFlashcards = [];
    const drawnFlashcardsFromElement = element.drawnFlashcards as [];
    for (const drawnFlashcard of drawnFlashcardsFromElement) {
      learnConfiguration.drawnFlashcards.push(this.adaptFlashcardForLearn(drawnFlashcard));
    }
    // Adapt actual flashcards for learn
    learnConfiguration.flashcards = [];
    const flashcardsFromElement = element.flashcardsForLearn as Array<any>;
    for (const flashcard of flashcardsFromElement) {
      if (flashcard.flashcardType === 'presentation') {
        learnConfiguration.flashcards.push(this.adaptFlashcardForLearnPresentation(flashcard));
      }
      if (flashcard.flashcardType === 'selection') {
        learnConfiguration.flashcards.push(this.adaptFlashcardLearnForSelection(flashcard));
      }
      if (flashcard.flashcardType === 'input') {
        learnConfiguration.flashcards.push(this.adaptFlashcardForLearnInput(flashcard));
      }
    }
    // Adapt learn type
    if (element.learnType === 0) {
      learnConfiguration.learnType = LearnTypeEnum.Learn;
    }
    if (element.learnType === 1) {
      learnConfiguration.learnType = LearnTypeEnum.Repetition;
    }
    if (element.learnType === 2) {
      learnConfiguration.learnType = LearnTypeEnum.HardWords;
    }
    learnConfiguration.lessonName = element.lessonName;
    return learnConfiguration;
  }

  adaptFlashcardForLearn(element: any): FlashcardForLearn {
    const flashcardForLearn = new FlashcardForLearn();
    flashcardForLearn.flashcardId = element.flashcardId;
    flashcardForLearn.flashcardSubscriptionId = element.flashcardSubscriptionId;
    flashcardForLearn.phrase = element.phrase;
    flashcardForLearn.phrasePronunciation = element.phrasePronunciation;
    flashcardForLearn.phraseSampleSentence = element.phraseSampleSentence;
    flashcardForLearn.phraseComment = element.phraseComment;
    flashcardForLearn.translatedPhrase = element.translatedPhrase;
    flashcardForLearn.translatedPhraseSampleSentence = element.translatedPhraseSampleSentence;
    flashcardForLearn.translatedPhraseComment = element.translatedPhraseComment;
    flashcardForLearn.trainLevel = element.trainLevel;
    flashcardForLearn.markedAsHard = element.markedAsHard;
    flashcardForLearn.lastTrainingDate = element.lastTrainingDate;
    return flashcardForLearn;
  }

  adaptFlashcardForLearnPresentation(element: any): FlashcardLearnForPresentation {
    const flashcardLearnForPresentation = new FlashcardLearnForPresentation();
    flashcardLearnForPresentation.flashcardId = element.flashcardId;
    flashcardLearnForPresentation.flashcardSubscriptionId = element.flashcardSubscriptionId;
    flashcardLearnForPresentation.phrase = element.phrase;
    flashcardLearnForPresentation.phrasePronunciation = element.phrasePronunciation;
    flashcardLearnForPresentation.phraseSampleSentence = element.phraseSampleSentence;
    flashcardLearnForPresentation.phraseComment = element.phraseComment;
    flashcardLearnForPresentation.translatedPhrase = element.translatedPhrase;
    flashcardLearnForPresentation.translatedPhraseSampleSentence = element.translatedPhraseSampleSentence;
    flashcardLearnForPresentation.translatedPhraseComment = element.translatedPhraseComment;
    flashcardLearnForPresentation.trainLevel = element.trainLevel;
    flashcardLearnForPresentation.markedAsHard = element.markedAsHard;
    flashcardLearnForPresentation.lastTrainingDate = element.lastTrainingDate;
    flashcardLearnForPresentation.withInput = element.withInput;
    return flashcardLearnForPresentation;
  }

  adaptFlashcardForLearnInput(element: any): FlashcardLearnForInput {
    const flashcardLearnForInput = new FlashcardLearnForInput();
    flashcardLearnForInput.flashcardId = element.flashcardId;
    flashcardLearnForInput.flashcardSubscriptionId = element.flashcardSubscriptionId;
    flashcardLearnForInput.phrase = element.phrase;
    flashcardLearnForInput.phrasePronunciation = element.phrasePronunciation;
    flashcardLearnForInput.phraseSampleSentence = element.phraseSampleSentence;
    flashcardLearnForInput.phraseComment = element.phraseComment;
    flashcardLearnForInput.translatedPhrase = element.translatedPhrase;
    flashcardLearnForInput.translatedPhraseSampleSentence = element.translatedPhraseSampleSentence;
    flashcardLearnForInput.translatedPhraseComment = element.translatedPhraseComment;
    flashcardLearnForInput.trainLevel = element.trainLevel;
    flashcardLearnForInput.markedAsHard = element.markedAsHard;
    flashcardLearnForInput.lastTrainingDate = element.lastTrainingDate;
    return flashcardLearnForInput;
  }

  adaptFlashcardLearnForSelection(element: any): FlashcardLearnForSelection {
    const flashcardLearnForSelection = new FlashcardLearnForSelection();
    flashcardLearnForSelection.flashcardId = element.flashcardId;
    flashcardLearnForSelection.flashcardSubscriptionId = element.flashcardSubscriptionId;
    flashcardLearnForSelection.phrase = element.phrase;
    flashcardLearnForSelection.phrasePronunciation = element.phrasePronunciation;
    flashcardLearnForSelection.phraseSampleSentence = element.phraseSampleSentence;
    flashcardLearnForSelection.phraseComment = element.phraseComment;
    flashcardLearnForSelection.translatedPhrase = element.translatedPhrase;
    flashcardLearnForSelection.translatedPhraseSampleSentence = element.translatedPhraseSampleSentence;
    flashcardLearnForSelection.translatedPhraseComment = element.translatedPhraseComment;
    flashcardLearnForSelection.trainLevel = element.trainLevel;
    flashcardLearnForSelection.markedAsHard = element.markedAsHard;
    flashcardLearnForSelection.lastTrainingDate = element.lastTrainingDate;
    flashcardLearnForSelection.correctPhrase = element.correctPhrase;
    flashcardLearnForSelection.flashcardsForSelection = new Array<string>();
    const flashcardsSelectionFromElement = element.flashcardsForSelection as [];
    for (const el of flashcardsSelectionFromElement) {
      flashcardLearnForSelection.flashcardsForSelection.push(el);
    }
    return flashcardLearnForSelection;
  }
}
