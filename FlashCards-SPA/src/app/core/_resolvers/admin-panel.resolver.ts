import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {CourseForCheck} from '../_models/_dtos/fromServer/courseForCheck';
import {Observable, of} from 'rxjs';
import {AlertifyService} from '../_services/alertify.service';
import {AdministrationService} from '../_services/administration.service';
import {catchError} from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdminPanelResolver implements Resolve<Array<CourseForCheck>> {

  constructor(private adminService: AdministrationService,
              private router: Router,
              private alertifyService: AlertifyService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Array<CourseForCheck>> {
    return this.adminService.getCoursesForCheck().pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        this.router.navigate(['/courses']);
        return of(null);
      })
    );
  }

}
