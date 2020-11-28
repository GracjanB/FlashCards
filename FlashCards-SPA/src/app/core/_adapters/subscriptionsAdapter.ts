import {Injectable} from '@angular/core';
import {SubscribedCourseShort} from '../_models/subscribedCourseShort';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionsAdapter {

  adaptSubscribedCourseShort(subscribedCourse: any): SubscribedCourseShort {
    return new SubscribedCourseShort(
      subscribedCourse.id,
      subscribedCourse.courseId,
      subscribedCourse.course.name,
      subscribedCourse.course.accountCreatedName,
      subscribedCourse.overallProgress,
      subscribedCourse.lastActivity
    );
  }
}
