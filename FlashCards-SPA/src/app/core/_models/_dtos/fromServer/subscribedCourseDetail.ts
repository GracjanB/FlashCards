import { SubscribedLessonShort } from './subscribedLessonShort';

export class SubscribedCourseDetail {
  subscriptionId: number;
  courseId: number;
  accountCreatedDisplayName: string;
  courseName: string;
  courseDescription: string;
  courseType: string;
  amountOfEnrolled: number;
  isSubscribing: boolean;
  amountOfFlashcards: number;
  amountOfFlashcardsLearnt: number;
  overallProgress: number;
  lastActivity: Date;
  lessons: SubscribedLessonShort[];

  constructor(subscriptionId: number,
              courseId: number,
              accountCreatedDisplayName: string,
              courseName: string,
              courseDescription: string,
              courseType: string,
              amountOfEnrolled: number,
              isSubscribing: boolean,
              amountOfFlashcards: number,
              amountOfFlashcardsLearnt: number,
              overallProgress: number,
              lastActivity: Date,
              lessons: SubscribedLessonShort[]) {
    this.subscriptionId = subscriptionId;
    this.courseId = courseId;
    this.accountCreatedDisplayName = accountCreatedDisplayName;
    this.courseName = courseName;
    this.courseDescription = courseDescription;
    this.courseType = courseType;
    this.amountOfEnrolled = amountOfEnrolled;
    this.isSubscribing = isSubscribing;
    this.amountOfFlashcards = amountOfFlashcards;
    this.amountOfFlashcardsLearnt = amountOfFlashcardsLearnt;
    this.overallProgress = overallProgress;
    this.lastActivity = lastActivity;
    this.lessons = lessons;
  }
}
