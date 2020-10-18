import { Component, OnInit } from '@angular/core';
import {Course} from '../core/_models/_dtos/fromServer/course';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  course: Course;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.course = data.course;
    });
  }

}
