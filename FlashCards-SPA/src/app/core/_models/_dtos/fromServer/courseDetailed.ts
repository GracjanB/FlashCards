import {Course} from './course';
import {LessonShort} from './lessonShort';

export class CourseDetailed extends Course {
  lessons: LessonShort[];

  constructor(id: number, name: string, description: string, dateCreated: string,
              numberOfEnrolled: string, authorDisplayName: string,
              numberOfRatings: number, averageRating: number,
              lessons: LessonShort[]) {
    super(id, name, description, dateCreated, numberOfEnrolled,
          authorDisplayName, numberOfRatings, averageRating);
    this.lessons = lessons;
  }
}
