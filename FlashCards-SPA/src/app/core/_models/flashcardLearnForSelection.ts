import {FlashcardForLearn} from './_dtos/fromServer/flashcardForLearn';

export class FlashcardLearnForSelection extends FlashcardForLearn {
  public readonly flashcardType = 'selection';
  flashcardsForSelection: Array<string>;
  correctPhrase: string;
  ordinalNumber: number;
}
