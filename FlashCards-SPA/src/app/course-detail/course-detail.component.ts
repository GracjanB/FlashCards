import { Component, OnInit } from '@angular/core';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {ActivatedRoute} from '@angular/router';
import {CourseDetailed} from '../core/_models/_dtos/fromServer/courseDetailed';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  course: CourseDetailed;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.course = data.course;
      console.log(this.course);
    });
  }

}
