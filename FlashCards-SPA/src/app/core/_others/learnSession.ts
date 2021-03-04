import {FlashcardForLearn} from '../_models/_dtos/fromServer/flashcardForLearn';
import {FlashcardLearnForPresentation} from '../_models/flashcardLearnForPresentation';
import {LearnSummary} from '../_models/learnSummary';
import {LearnTypeEnum} from '../_models/enums/learnTypeEnum';

export class LearnSession {
  private readonly drawnFlashcards: Array<FlashcardForLearn>;
  private readonly drawnFlashcardsBeforeLearn: Array<FlashcardForLearn>;
  private learnSummary: LearnSummary;
  private flashcards: Array<any>;
  private currentFlashcard: any;
  private numberOfMoves: number;
  private currentIndex: number;
  private learnType: LearnTypeEnum;
  public canContinue: boolean;

  constructor(drawnFlashcards: FlashcardForLearn[], flashcards: any[], learnType: LearnTypeEnum) {
    this.drawnFlashcards = new Array<FlashcardForLearn>();
    this.drawnFlashcardsBeforeLearn = new Array<FlashcardForLearn>();
    for (const drawnFlashcard of drawnFlashcards) {
      this.drawnFlashcards.push(drawnFlashcard);
      const flashcardBeforeLearn = new FlashcardForLearn();
      flashcardBeforeLearn.initializeWithFlashcard(drawnFlashcard);
      this.drawnFlashcardsBeforeLearn.push(flashcardBeforeLearn);
    }
    this.flashcards = new Array<any>();
    for (const flashcard of flashcards) {
      this.flashcards.push(flashcard);
    }
    this.learnType = learnType;
  }

  startLearning(): void {
    this.numberOfMoves = 0;
    this.currentIndex = 0;
    this.currentFlashcard = this.flashcards[0];
    this.canContinue = true;
  }

  getCurrentFlashcard(): any {
    return this.currentFlashcard;
  }

  getLearnSummary(): LearnSummary {
    return this.learnSummary;
  }

  nextFlashcard(result: boolean): any {
    if (result) {
      const flashcard1 = this.drawnFlashcards.find(x => x.flashcardId === this.currentFlashcard.flashcardId);
      const flashcardIndex = this.drawnFlashcards.indexOf(flashcard1);

      if (this.learnType === LearnTypeEnum.Learn) {
        const calculatedPoints = this.calculatePoints(this.currentFlashcard);
        this.drawnFlashcards[flashcardIndex].trainLevel += calculatedPoints;
      }
    }
    else {
      const flashcard = this.drawnFlashcards.find(x => x.flashcardId === this.currentFlashcard.flashcardId);
      const flashcardForPresentation = this.createFlashcardForPresentation(flashcard);
      const indexOfCurrentFlashcard = this.flashcards.indexOf(this.currentFlashcard);
      this.insertIntoFlashcards(flashcardForPresentation, indexOfCurrentFlashcard);
    }

    this.currentIndex = this.currentIndex + 1;
    this.numberOfMoves = this.numberOfMoves + 1;
    if (this.currentIndex === this.flashcards.length - 1) {
      this.currentFlashcard = null;
      this.learnSummary = this.createLearnSummary();
      this.canContinue = false;
    } else {
      this.currentFlashcard = this.flashcards[this.currentIndex];
      return this.currentFlashcard;
    }
  }

  private insertIntoFlashcards(element: any, index: number): void {
    let firstPart = [];
    if (index === 0) {
      firstPart.push(this.flashcards[index]);
    } else {
      firstPart = this.flashcards.slice(0, index + 1);
    }
    const secondPart = this.flashcards.slice(index + 1, this.flashcards.length);
    firstPart.push(element);
    this.flashcards = firstPart.concat(secondPart);
  }

  private calculatePoints(flashcard: any): number {
    if (flashcard.flashcardType === 'presentation') {
      return 0;
    }
    if (flashcard.flashcardType === 'selection') {
      return 1;
    }
    if (flashcard.flashcardType === 'input') {
      return 2;
    }
    if (flashcard.flashcardType === 'blocks') {
      return 1;
    }
  }

  private createFlashcardForPresentation(flashcardForLearn: FlashcardForLearn): FlashcardLearnForPresentation {
    const flashcard = new FlashcardLearnForPresentation();
    flashcard.initializeWithFlashcard(flashcardForLearn);
    flashcard.withInput = true; // important!
    return flashcard;
  }

  private createLearnSummary(): LearnSummary {
    const learnSummary = new LearnSummary();
    learnSummary.flashcardsBeforeLearn = this.drawnFlashcardsBeforeLearn;
    learnSummary.flashcardsAfterLearn = this.drawnFlashcards;
    return learnSummary;
  }
}
