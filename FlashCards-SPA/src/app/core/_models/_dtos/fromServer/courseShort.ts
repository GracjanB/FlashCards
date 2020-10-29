export class Course {
  id: number;
  name: string;
  description: string;
  dateCreated: Date;
  numberOfEnrolled: string;
  authorDisplayName: string;
  numberOfRatings: number;
  averageRating: number;

  constructor(id: number, name: string, description: string, dateCreated: string,
              numberOfEnrolled: string, authorDisplayName: string,
              numberOfRatings: number, averageRating: number) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.dateCreated = new Date(dateCreated);
    this.numberOfEnrolled = numberOfEnrolled;
    this.authorDisplayName = authorDisplayName;
    this.numberOfRatings = numberOfRatings;
    this.averageRating = averageRating;
  }
}
