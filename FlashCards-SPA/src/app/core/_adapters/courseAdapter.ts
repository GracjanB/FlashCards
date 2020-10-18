import { Injectable } from '@angular/core';
import {Course} from '../_models/_dtos/fromServer/course';

@Injectable({
  providedIn: 'root'
})
export class CourseAdapter {

  adaptCourse(course: any): Course {
    return new Course(
      course.id,
      course.name,
      course.description,
      course.dateCreated,
      course.numberOfEnrolled,
      course.authorDisplayName,
      course.numberOfRatings,
      course.averageRating);
  }
}
