export class CourseParams {
  courseType: number;
  searchedTitle: string;

  constructor(courseType: number, searchedTitle: string = '') {
    this.courseType = courseType;
    this.searchedTitle = searchedTitle;
  }
}
