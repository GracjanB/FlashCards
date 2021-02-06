import {FlashcardForLearn} from './_dtos/fromServer/flashcardForLearn';

export class FlashcardLearnForPresentation extends FlashcardForLearn {
  public readonly flashcardType = 'presentation';
  withInput: boolean;
  ordinalNumber: number;
}
