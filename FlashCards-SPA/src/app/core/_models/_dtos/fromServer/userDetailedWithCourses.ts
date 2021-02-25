import {User} from './user';
import {CourseShort} from './courseShort';

export class UserDetailedWithCourses extends User {
  createdCourses: CourseShort[];
  subscribedCourses: CourseShort[];
  privateCourses: CourseShort[];
  draftCourses: CourseShort[];
  numberOfCreatedCourses: number;
  numberOfSubscribedCourses: number;
  numberOfAlreadyLearntFlashcards: number;

  constructor(id: number,
              email: string,
              displayName: string,
              photoUrl: string,
              firstName: string,
              lastName: string,
              city: string,
              country: string,
              dateCreated: Date,
              createdCourses: CourseShort[],
              subscribedCourses: CourseShort[],
              privateCourses: CourseShort[],
              draftCourses: CourseShort[],
              numberOfCreatedCourses: number,
              numberOfSubscribedCourses: number,
              numberOfAlreadyLearntFlashcards: number) {
    super();
    this.id = id;
    this.email = email;
    this.displayName = displayName;
    this.photoUrl = photoUrl;
    this.firstName = firstName;
    this.lastName = lastName;
    this.city = city;
    this.country = country;
    this.dateCreated = dateCreated;
    this.createdCourses = createdCourses;
    this.subscribedCourses = subscribedCourses;
    this.privateCourses = privateCourses;
    this.draftCourses = draftCourses;
    this.numberOfCreatedCourses = numberOfCreatedCourses;
    this.numberOfSubscribedCourses = numberOfSubscribedCourses;
    this.numberOfAlreadyLearntFlashcards = numberOfAlreadyLearntFlashcards;
  }
}
