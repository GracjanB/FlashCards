import {Component, Input, OnInit} from '@angular/core';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {Router} from '@angular/router';

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent implements OnInit {
  @Input() course: CourseShort;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onCourseClick() {
    const url = '/courses/' + this.course.id;
    this.router.navigate([url]);
  }
}
