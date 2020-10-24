export class FlashcardShort {
  id: number;
  phrase: string;
  translatedPhrase: string;

  constructor(id: number, phrase: string, translatedPhrase: string) {
    this.id = id;
    this.phrase = phrase;
    this.translatedPhrase = translatedPhrase;
  }
}
