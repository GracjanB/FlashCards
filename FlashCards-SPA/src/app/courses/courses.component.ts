import { Component, OnInit } from '@angular/core';
import {CourseService} from '../core/_services/course.service';
import {PaginatedResult, Pagination} from '../core/_models/common/pagination';
import {CourseParams} from '../core/_models/_dtos/toServer/courseParams';
import {AlertifyService} from '../core/_services/alertify.service';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: CourseShort[];
  pagination: Pagination;
  searchedTitle: string;
  isContentFiltered = false;

  constructor(private courseService: CourseService,
              private alertifyService: AlertifyService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.courses = data.courses.result;
      this.pagination = data.courses.pagination;
    });
  }

  getCourses(page: number = 1, itemsPerPage: number = 8, searchedTitle: string = '') {
    const courseParams = new CourseParams(0);
    courseParams.searchedTitle = searchedTitle;
    this.courseService.getCourses(page, itemsPerPage, courseParams).subscribe(
      (result: PaginatedResult<CourseShort[]>) => {
        this.courses = result.result;
        this.pagination = result.pagination;
      }, error => {
        this.alertifyService.showErrorAlert(error);
      }
    );
  }

  pageChanged(pagination: any): void {
    if (this.isContentFiltered) {
      this.getCourses(pagination.page, pagination.itemsPerPage, this.searchedTitle);
    } else {
      this.getCourses(pagination.page, pagination.itemsPerPage);
    }
  }

  navigateToCourseGenerator(): void {
    this.router.navigate(['/course-generator']);
  }

  search(): void {
    this.isContentFiltered = true;
    this.getCourses(1, 8, this.searchedTitle);
  }

  clearSearching(): void {
    this.isContentFiltered = false;
    this.searchedTitle = '';
    this.getCourses();
  }
}
