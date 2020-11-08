import {CourseShort} from './courseShort';
import {LessonShort} from './lessonShort';

export class CourseDetailed extends CourseShort {
  lessons: LessonShort[];

  constructor(id: number, name: string, description: string, dateCreated: string,
              numberOfEnrolled: string, authorDisplayName: string,
              numberOfRatings: number, averageRating: number, courseType: number,
              lessons: LessonShort[]) {
    super(id, name, description, dateCreated, numberOfEnrolled,
          authorDisplayName, numberOfRatings, averageRating, courseType);
    this.lessons = lessons;
  }
}
