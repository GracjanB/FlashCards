import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {Course} from '../_models/_dtos/fromServer/course';
import {Injectable} from '@angular/core';
import {CourseService} from '../_services/course.service';
import {AlertifyService} from '../_services/alertify.service';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';

@Injectable()
export class CourseDetailResolver implements Resolve<Course> {

  constructor(private courseService: CourseService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Course> {
    return this.courseService.getCourse(route.params.id).pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        this.router.navigate(['/courses']);
        return of(null);
      })
    );
  }
}
