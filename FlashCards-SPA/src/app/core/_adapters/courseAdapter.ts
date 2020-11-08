import { Injectable } from '@angular/core';
import { CourseShort } from '../_models/_dtos/fromServer/courseShort';
import { LessonAdapter } from './lessonAdapter';
import { CourseDetailed } from '../_models/_dtos/fromServer/courseDetailed';
import { LessonShort } from '../_models/_dtos/fromServer/lessonShort';
import {CourseForCreate} from '../_models/_dtos/toServer/courseForCreate';
import {CourseForUpdate} from '../_models/_dtos/toServer/courseForUpdate';

@Injectable({
  providedIn: 'root'
})
export class CourseAdapter {

  constructor(private lessonAdapter: LessonAdapter) { }

  adaptCourse(course: any): CourseShort {
    return new CourseShort(
      course.id,
      course.name,
      course.description,
      course.dateCreated,
      course.numberOfEnrolled,
      course.authorDisplayName,
      course.numberOfRatings,
      course.averageRating,
      course.courseType);
  }

  adaptCourseDetailed(course: any): CourseDetailed {
    const lessonsFromArg = course.lessons as [];
    const lessons: LessonShort[] = [];
    for (const lesson of lessonsFromArg) {
      lessons.push(this.lessonAdapter.adaptLessonShort(lesson, course.id));
    }

    return new CourseDetailed(
      course.id,
      course.name,
      course.description,
      course.dateCreated,
      course.numberOfEnrolled,
      course.authorDisplayName,
      course.numberOfRatings,
      course.averageRating,
      course.courseType,
      lessons);
  }

  adaptCourseForCreate(course: any): CourseForCreate {
    return new CourseForCreate(course.name, course.description, Number(course.courseType));
  }

  adaptCourseForUpdate(course: any): CourseForUpdate {
    return new CourseForUpdate(course.name, course.description, Number(course.courseType));
  }
}
