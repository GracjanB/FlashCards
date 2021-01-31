export class FlashcardShort {
  id: number;
  subscribedFlashcardId: number;
  phrase: string;
  translatedPhrase: string;
  isSubscribed: boolean;
  progress: number;
  markedAsHard: boolean;
  markedAsIgnored: boolean;

  constructor(id: number,
              subscribedFlashcardId: number,
              phrase: string,
              translatedPhrase: string,
              isSubscribed: boolean,
              progress: number,
              markedAsHard: boolean,
              markedAsIgnored: boolean) {
    this.id = id;
    this.subscribedFlashcardId = subscribedFlashcardId;
    this.phrase = phrase;
    this.translatedPhrase = translatedPhrase;
    this.isSubscribed = isSubscribed;
    this.progress = progress;
    this.markedAsHard = markedAsHard;
    this.markedAsIgnored = markedAsIgnored;
  }
}
