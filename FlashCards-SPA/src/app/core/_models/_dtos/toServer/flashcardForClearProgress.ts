export class FlashcardForClearProgress {
  subscribedFlashcardId: number;
  accountId: number;

  constructor(subscribedFlashcardId: number,
              accountId: number) {
    this.subscribedFlashcardId = subscribedFlashcardId;
    this.accountId = accountId;
  }
}
