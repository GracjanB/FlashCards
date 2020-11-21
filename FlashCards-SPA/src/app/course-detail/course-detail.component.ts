import { Component, OnInit } from '@angular/core';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseDetailed} from '../core/_models/_dtos/fromServer/courseDetailed';
import {LessonShort} from '../core/_models/_dtos/fromServer/lessonShort';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  course: CourseDetailed;

  constructor(private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.course = data.course;
      console.log(this.course);
    });
  }

  onLessonSelected(lesson: LessonShort): void {
    const url = 'courses/' + this.course.id + '/lessons/' + lesson.id;
    this.router.navigate([url]);
  }

}
