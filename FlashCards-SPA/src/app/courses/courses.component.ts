import { Component, OnInit } from '@angular/core';
import {CourseService} from '../core/_services/course.service';
import {PaginatedResult, Pagination} from '../core/_models/common/pagination';
import {CourseParams} from '../core/_models/_dtos/toServer/courseParams';
import {AlertifyService} from '../core/_services/alertify.service';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: CourseShort[];
  pagination: Pagination;

  constructor(private courseService: CourseService,
              private alertifyService: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.courses = data.courses.result;
      this.pagination = data.courses.pagination;
    });
    console.log('This is from ngInit:');
    console.log(this.courses);
    console.log(this.pagination);
  }

  getCourses() {
    const courseParams = new CourseParams(0);
    this.courseService.getCourses(1, 10, courseParams).subscribe(
      (result: PaginatedResult<CourseShort[]>) => {
        this.courses = result.result;
        this.pagination = result.pagination;
      }, error => {
        this.alertifyService.showErrorAlert(error);
      }
    );
  }
}
