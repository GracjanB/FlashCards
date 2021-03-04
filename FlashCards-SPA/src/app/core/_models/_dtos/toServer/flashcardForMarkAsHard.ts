export class FlashcardForMarkAsHard {
  subscribedFlashcardId: number;
  accountId: number;
  markedAsHard: boolean;

  constructor(subscribedFlashcardId: number,
              accountId: number,
              markedAsHard: boolean) {
    this.subscribedFlashcardId = subscribedFlashcardId;
    this.accountId = accountId;
    this.markedAsHard = markedAsHard;
  }
}
