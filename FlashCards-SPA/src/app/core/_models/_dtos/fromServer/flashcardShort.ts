export class FlashcardShort {
  id: number;
  phrase: string;
  translatedPhrase: string;
  isSubscribed: boolean;
  progress: number;
  markedAsHard: boolean;

  constructor(id: number,
              phrase: string,
              translatedPhrase: string,
              isSubscribed: boolean,
              progress: number,
              markedAsHard: boolean) {
    this.id = id;
    this.phrase = phrase;
    this.translatedPhrase = translatedPhrase;
    this.isSubscribed = isSubscribed;
    this.progress = progress;
    this.markedAsHard = markedAsHard;
  }
}
