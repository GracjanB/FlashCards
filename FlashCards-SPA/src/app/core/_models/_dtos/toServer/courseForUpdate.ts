export class CourseForUpdate {
  name: string;
  description: string;
  courseType: number;

  constructor(name: string, description: string, courseType: number) {
    this.name = name;
    this.description = description;
    this.courseType = courseType;
  }
}
