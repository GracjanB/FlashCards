import { FlashcardForLearn } from './_dtos/fromServer/flashcardForLearn';

export class FlashcardLearnForInput extends FlashcardForLearn {
  public readonly flashcardType = 'input';
  ordinalNumber: number;
}
