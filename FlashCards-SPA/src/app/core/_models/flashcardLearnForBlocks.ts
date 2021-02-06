import { FlashcardForLearn } from './_dtos/fromServer/flashcardForLearn';

export class FlashcardLearnForBlocks extends FlashcardForLearn {
  public readonly flashcardType = 'blocks';
  dividedFlashcardPhraseShuffled: Array<string>;
  dividedFlashcardPhraseCorrect: Array<string>;
  ordinalNumber: number;
}
