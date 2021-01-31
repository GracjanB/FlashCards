export class FlashcardForMarkAsIgnored {
  subscribedFlashcardId: number;
  accountId: number;
  markAsIgnored: boolean;

  constructor(subscribedFlashcardId: number,
              accountId: number,
              markAsIgnored: boolean) {
    this.subscribedFlashcardId = subscribedFlashcardId;
    this.accountId = accountId;
    this.markAsIgnored = markAsIgnored;
  }
}
