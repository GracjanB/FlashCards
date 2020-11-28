export class CourseShort {
  id: number;
  courseName: string;
  courseDescription: string;
  dateCreated: Date;
  numberOfEnrolled: string;
  courseType: number;
  accountCreatedDisplayName: string;
  numberOfRatings: number;
  averageRating: number;

  constructor(id: number, name: string, description: string, dateCreated: string,
              numberOfEnrolled: string, authorDisplayName: string,
              numberOfRatings: number, averageRating: number, courseType: number) {
    this.id = id;
    this.courseName = name;
    this.courseDescription = description;
    this.dateCreated = new Date(dateCreated);
    this.numberOfEnrolled = numberOfEnrolled;
    this.accountCreatedDisplayName = authorDisplayName;
    this.numberOfRatings = numberOfRatings;
    this.averageRating = averageRating;
    this.courseType = courseType;
  }
}
