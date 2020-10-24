import { Injectable } from '@angular/core';
import { Course } from '../_models/_dtos/fromServer/course';
import { LessonAdapter } from './lessonAdapter';
import { CourseDetailed } from '../_models/_dtos/fromServer/courseDetailed';
import { LessonShort } from '../_models/_dtos/fromServer/lessonShort';

@Injectable({
  providedIn: 'root'
})
export class CourseAdapter {

  constructor(private lessonAdapter: LessonAdapter) { }

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
      lessons);
  }
}
