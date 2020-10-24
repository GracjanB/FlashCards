import {Injectable} from '@angular/core';
import {FlashcardShort} from '../_models/_dtos/fromServer/flashcardShort';
import {Flashcard} from '../_models/_dtos/fromServer/flashcard';
import {MapExtensions} from '../_extensions/mapExtensions';

@Injectable({
  providedIn: 'root'
})
export class FlashcardAdapter {

  constructor(private mapExtensions: MapExtensions) {}

  adaptFlashcardShort(flashcard: any): FlashcardShort {
    return new FlashcardShort(flashcard.id, flashcard.phrase, flashcard.translatedPhrase);
  }

  adaptFlashcard(flashcard: any): Flashcard {
    return new Flashcard(
      flashcard.id,
      flashcard.phrase,
      flashcard.phrasePronunciation,
      flashcard.phraseSampleSentence,
      flashcard.phraseComment,
      flashcard.translatedPhrase,
      flashcard.translatedPhraseSampleSentence,
      flashcard.translatedPhraseComment,
      flashcard.languageLevel,
      flashcard.category
    );
  }
}
