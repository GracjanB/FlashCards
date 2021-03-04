import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {CourseForCheck} from '../_models/_dtos/fromServer/courseForCheck';
import {Injectable} from '@angular/core';
import {AdministrationService} from '../_services/administration.service';
import {AlertifyService} from '../_services/alertify.service';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminCourseCheckResolver implements Resolve<CourseForCheck> {

  constructor(private adminService: AdministrationService,
              private router: Router,
              private alertifyService: AlertifyService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<CourseForCheck> {
    return this.adminService.getCourseForCheck(route.params.id).pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        this.router.navigate(['/courses']);
        return of(null);
      })
    );
  }
}
