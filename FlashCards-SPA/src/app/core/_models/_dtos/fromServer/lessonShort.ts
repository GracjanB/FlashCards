export class LessonShort {
  id: number;
  name: string;
  category: string;
  courseId: number;

  constructor(id: number, name: string, category: string, courseId: number) {
    this.id = id;
    this.name = name;
    this.category = category;
    this.courseId = courseId;
  }
}
