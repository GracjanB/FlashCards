export class FlashcardForMarkAsHard {
  subscribedFlashcardId: number;
  accountId: number;
  markAsHard: boolean;

  constructor(subscribedFlashcardId: number,
              accountId: number,
              markAsHard: boolean) {
    this.subscribedFlashcardId = subscribedFlashcardId;
    this.accountId = accountId;
    this.markAsHard = markAsHard;
  }
}
