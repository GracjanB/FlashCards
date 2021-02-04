import {Injectable} from '@angular/core';
import {FlashcardForLearn} from '../_models/_dtos/fromServer/flashcardForLearn';
import {LinkedList} from 'typescript-collections';
import {forEach} from 'typescript-collections/dist/lib/arrays';
import {FlashcardLearnForPresentation} from '../_models/flashcardLearnForPresentation';
import {FlashcardLearnForInput} from '../_models/flashcardLearnForInput';
import {FlashcardLearnForSelection} from '../_models/flashcardLearnForSelection';

@Injectable({
  providedIn: 'root'
})
export class LearningSessionServiceService {
  private static readonly sumOfPoints = 5;
  private flashcardsForLearn: Array<any>;
  private testFlashcardsForLearn: Array<any>;

  constructor() {
    this.testFlashcardsForLearn = this.loadDesignData();
    this.flashcardsForLearn = new Array<any>();
  }


  spliceArray2(): void {

  }

  spliceArray(): void {
    const flashcards = this.drawFlashcards(this.testFlashcardsForLearn);
    console.log(flashcards);
  }

  private getRandomIntInclusive(min, max): number {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  private drawFlashcards(flashcards: Array<FlashcardForLearn>): Array<any> {
    const outputFlashcards = new Array<any>();

    forEach(flashcards, (flashcard) => {
      let remainedSum = 5;
      if (flashcard.trainLevel === 0) {
        outputFlashcards.push(this.createFlashcardPresentation(flashcard));
        remainedSum -= 1;
      }
      if (flashcard.trainLevel >= 6) {
        outputFlashcards.push(this.createFlashcardInput(flashcard));
        return;
      }

      while (remainedSum > 0) {
        if (remainedSum === 1) {
          outputFlashcards.push(this.createFlashcardSelection(flashcard));
          return;
        } else {
          const randomNumber = this.getRandomIntInclusive(0, 10);
          if (randomNumber < 5) {
            outputFlashcards.push(this.createFlashcardSelection(flashcard));
            remainedSum -= 1;
          } else {
            outputFlashcards.push(this.createFlashcardInput(flashcard));
            remainedSum -= 2;
          }
        }
      }
    });

    return outputFlashcards;
  }

  private createFlashcardInput(flashcard: FlashcardForLearn): FlashcardLearnForInput {
    const flashcardInput = new FlashcardLearnForInput();
    flashcardInput.initializeWithFlashcard(flashcard);
    return flashcardInput;
  }

  private createFlashcardPresentation(flashcard: FlashcardForLearn): FlashcardLearnForPresentation {
    const flashcardPresentation = new FlashcardLearnForPresentation();
    flashcardPresentation.initializeWithFlashcard(flashcard);
    return flashcardPresentation;
  }

  private createFlashcardSelection(flashcard: FlashcardForLearn): FlashcardLearnForSelection {
    const flashcardSelection = new FlashcardLearnForSelection();
    flashcardSelection.initializeWithFlashcard(flashcard);

    let wordsForSelection = new Array<string>();
    forEach(this.testFlashcardsForLearn, (flashcardElement) => {
      if (flashcardElement === flashcard) {
        return;
      } else {
        wordsForSelection.push(flashcardElement.translatedPhrase);
      }
    });

    for (let i = 0; i < 3; i++) {
      wordsForSelection = this.shuffle(wordsForSelection);
    }
    wordsForSelection = wordsForSelection.slice(0, 3);
    wordsForSelection.push(flashcard.translatedPhrase);
    for (let i = 0; i < 3; i++) {
      wordsForSelection = this.shuffle(wordsForSelection);
    }
    flashcardSelection.flashcardsForSelection = wordsForSelection;

    return flashcardSelection;
  }

  private shuffle(array: Array<any>): Array<any> {
    let currentIndex = array.length;
    let temporaryValue;
    let randomIndex;

    while (0 !== currentIndex) {
      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex -= 1;
      temporaryValue = array[currentIndex];
      array[currentIndex] = array[randomIndex];
      array[randomIndex] = temporaryValue;
    }

    return array;
  }

  loadDesignData(): Array<FlashcardForLearn> {
    const flashcardsForLearnArray = new Array<FlashcardForLearn>();
    const flashcard1 = new FlashcardForLearn();
    flashcard1.initialize(
      0,
      0,
      'addictive',
      'adiktiw',
      'I am so addictive',
      'Comment',
      'uzależniający',
      'Jestem bardzo uzależniający',
      'Komentarz',
      'B1',
      'Kategoria',
      3,
      true,
      new Date());
    const flashcard2 = new FlashcardForLearn();
    flashcard2.initialize(
      0,
      0,
      'to look for',
      'tu luk for',
      'I am looking for a job',
      'Comment',
      'szukać',
      'Szukam pracy',
      'Komentarz',
      'B2',
      'Kategoria',
      0,
      false,
      new Date());
    const flashcard3 = new FlashcardForLearn();
    flashcard3.initialize(
      0,
      0,
      'go after',
      'goł after',
      'Police is going after a robber',
      'Comment',
      'ścigać',
      'Policja ściga złodzieja',
      'Komentarz',
      'B2',
      'Kategoria',
      7,
      false,
      new Date());
    const flashcard4 = new FlashcardForLearn();
    flashcard4.initialize(
      0,
      0,
      'a meeting',
      'e miting',
      'A meeting with friends',
      'Comment',
      'spotkanie',
      'Spotkanie z przyjaciółmi',
      'Komentarz',
      'B2',
      'Kategoria',
      4,
      true,
      new Date());
    const flashcard5 = new FlashcardForLearn();
    flashcard5.initialize(
      0,
      0,
      'simplify',
      'sipmplifaj',
      'I would like to simplify the task',
      'Comment',
      'ułatwiać',
      'Chciałbym ułatwić to zadanie',
      'Komentarz',
      'B1',
      'Kategoria',
      1,
      false,
      new Date());
    flashcardsForLearnArray.push(flashcard1);
    flashcardsForLearnArray.push(flashcard2);
    flashcardsForLearnArray.push(flashcard3);
    flashcardsForLearnArray.push(flashcard4);
    flashcardsForLearnArray.push(flashcard5);
    return flashcardsForLearnArray;
  }
}
