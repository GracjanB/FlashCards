import {FlashcardForLearn} from './_dtos/fromServer/flashcardForLearn';

export class LearnSummary {
  flashcardsBeforeLearn: Array<FlashcardForLearn>;
  flashcardsAfterLearn: Array<FlashcardForLearn>;
}
