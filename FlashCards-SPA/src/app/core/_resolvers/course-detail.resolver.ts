import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CourseDetailed } from '../_models/_dtos/fromServer/courseDetailed';

@Injectable()
export class CourseDetailResolver implements Resolve<CourseDetailed> {

  constructor(private courseService: CourseService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<CourseDetailed> {
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
