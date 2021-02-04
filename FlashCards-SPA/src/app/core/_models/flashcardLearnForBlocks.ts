import { FlashcardForLearn } from './_dtos/fromServer/flashcardForLearn';

export class FlashcardLearnForBlocks extends FlashcardForLearn {
  dividedFlashcardPhraseShuffled: Array<string>;
  dividedFlashcardPhraseCorrect: Array<string>;
}
