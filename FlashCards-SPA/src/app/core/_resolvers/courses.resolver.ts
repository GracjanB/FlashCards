import {CourseShort} from '../_models/_dtos/fromServer/courseShort';
import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {CourseService} from '../_services/course.service';
import {AlertifyService} from '../_services/alertify.service';
import {Observable, of} from 'rxjs';
import {CourseParams} from '../_models/_dtos/toServer/courseParams';
import {catchError} from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable()
export class CoursesResolver implements Resolve<CourseShort[]> {
  private pageNumber = 1;
  private pageSize = 10;

  constructor(private courseService: CourseService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<CourseShort[]> {
    const courseParams = new CourseParams(0); // 0 means only public courses
    return this.courseService.getCourses(this.pageNumber, this.pageSize, courseParams).pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        this.router.navigate(['']);
        return of(null);
      })
    );
  }
}
