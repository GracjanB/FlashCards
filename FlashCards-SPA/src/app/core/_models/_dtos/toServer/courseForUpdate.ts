export class CourseForUpdate {
  id: number;
  name: string;
  description: string;
  courseType: number;

  constructor(name: string, description: string, courseType: number, id: number = 0) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.courseType = courseType;
  }
}
