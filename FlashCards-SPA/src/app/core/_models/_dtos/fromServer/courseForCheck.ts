import {LessonForCheck} from './lessonForCheck';

export class CourseForCheck {
  id: number;
  name: string;
  description: string;
  dateCreated: Date;
  accountCreatedName: string;
  lessons: Array<LessonForCheck>;

  constructor(id: number,
              name: string,
              description: string,
              dateCreated: Date,
              accountCreatedName: string,
              lessons: Array<LessonForCheck>) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.dateCreated = dateCreated;
    this.accountCreatedName = accountCreatedName;
    this.lessons = lessons;
  }
}
