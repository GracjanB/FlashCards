import { Component, OnInit } from '@angular/core';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseDetailed} from '../core/_models/_dtos/fromServer/courseDetailed';
import {LessonShort} from '../core/_models/_dtos/fromServer/lessonShort';
import {SubscribedCourseDetail} from '../core/_models/_dtos/fromServer/subscribedCourseDetail';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  // Course may be instance of SubscribedCourseDetail or CourseDetailed
  // depending on subscription by user
  course: any;
  isSubscribed: boolean;

  constructor(private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.course = data.course;
      this.prepareComponent();
    });
  }

  onLessonSelected(lesson: any): void {
    let url = '';
    if (this.course instanceof CourseDetailed){
      url = 'courses/' + this.course.id + '/lessons/' + lesson.id;
    } else {
      url = 'courses/' + this.course.courseId + '/lessons/' + lesson.id;
    }
    this.router.navigate([url]);
  }

  private prepareComponent(): void {
    if (this.course instanceof SubscribedCourseDetail) {
      this.isSubscribed = true;
    } else {
      this.isSubscribed = false;
    }
  }
}
