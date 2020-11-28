import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { SubscribedCourseShort } from '../_models/subscribedCourseShort';
import { SubscriptionsService } from '../_services/subscriptions.service';
import { Observable, of } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DashboardResolver implements Resolve<SubscribedCourseShort[]> {
  private pageNumber = 1;
  private pageSize = 5;

  constructor(private subscriptionsService: SubscriptionsService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<SubscribedCourseShort[]> {
    return this.subscriptionsService.getSubscribedCourses(this.pageNumber, this.pageSize).pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        this.router.navigate(['']);
        return of(null);
      })
    );
  }

}
