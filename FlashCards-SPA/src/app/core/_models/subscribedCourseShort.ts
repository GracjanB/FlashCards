export class SubscribedCourseShort {
  subscriptionId: number;
  courseId: number;
  courseName: string;
  courseAccountCreatedName: string;
  overallProgress: number;
  lastActivity: Date;

  constructor(subscriptionId: number, courseId: number, courseName: string,
              courseAccountCreatedName: string, overallProgress: number,
              lastActivity: Date) {
    this.subscriptionId = subscriptionId;
    this.courseId = courseId;
    this.courseName = courseName;
    this.courseAccountCreatedName = courseAccountCreatedName;
    this.overallProgress = overallProgress;
    this.lastActivity = lastActivity;
  }

}
