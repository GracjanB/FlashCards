import {Injectable} from '@angular/core';
import {CourseAdapter} from './courseAdapter';
import {UserDetailedWithCourses} from '../_models/_dtos/fromServer/userDetailedWithCourses';
import {CourseShort} from '../_models/_dtos/fromServer/courseShort';
import {UserDetailed} from '../_models/_dtos/fromServer/userDetailed';
import {UserForUpdate} from '../_models/_dtos/toServer/userForUpdate';
import {UserForPasswordChange} from '../_models/_dtos/toServer/userForPasswordChange';

@Injectable({
  providedIn: 'root'
})
export class UserAdapter {

  constructor(private courseAdapter: CourseAdapter) { }

  adaptUserForDetailWithCourses(userForDetail: any): UserDetailedWithCourses {
    const createdCoursesFromResponse = userForDetail.createdCourses as [];
    const createdCourses: CourseShort[] = [];
    for (const course of createdCoursesFromResponse) {
      createdCourses.push(this.courseAdapter.adaptCourse(course));
    }

    const subscribedCoursesFromResponse = userForDetail.subscribedCourses as [];
    const subscribedCourses: CourseShort[] = [];
    for (const course of subscribedCoursesFromResponse) {
      subscribedCourses.push(this.courseAdapter.adaptCourse(course));
    }

    return new UserDetailedWithCourses(userForDetail.id,
      userForDetail.email,
      userForDetail.displayName,
      userForDetail.photoUrl,
      userForDetail.firstName,
      userForDetail.lastName,
      userForDetail.city,
      userForDetail.country,
      userForDetail.dateCreated,
      createdCourses,
      subscribedCourses,
      userForDetail.numberOfCreatedCourses,
      userForDetail.numberOfSubscribedCourses,
      userForDetail.numberOfAlreadyLearntFlashcards);
  }

  adaptUserDetailed(userDetailed: any): UserDetailed {
    return new UserDetailed(
      userDetailed.id,
      userDetailed.accountId,
      userDetailed.email,
      userDetailed.displayName,
      userDetailed.photoUrl,
      userDetailed.firstName,
      userDetailed.lastName,
      userDetailed.city,
      userDetailed.country,
      userDetailed.dateCreated,
      userDetailed.numberOfWordsInLearningSession,
      userDetailed.numberOfWordsInReviewSession);
  }

  adaptUserForUpdate(userDetailed: UserDetailed): UserForUpdate {
    return new UserForUpdate(
      userDetailed.firstName,
      userDetailed.lastName,
      userDetailed.displayName,
      userDetailed.city,
      userDetailed.country,
      userDetailed.numberOfWordsInLearningSession,
      userDetailed.numberOfWordsInReviewSession);
  }

  adaptUserForPasswordChange(element: any): UserForPasswordChange {
    return new UserForPasswordChange(element.oldPassword, element.newPassword);
  }
}
