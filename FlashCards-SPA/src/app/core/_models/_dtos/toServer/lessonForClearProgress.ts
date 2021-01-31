export class LessonForClearProgress {
  subscribedLessonId: number;
  accountId: number;

  constructor(subscribedLessonId: number,
              accountId: number) {
    this.subscribedLessonId = subscribedLessonId;
    this.accountId = accountId;
  }
}
