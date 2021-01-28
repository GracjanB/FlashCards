import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {UserDetailed} from '../_models/_dtos/fromServer/userDetailed';
import {Observable, of} from 'rxjs';
import {UserService} from '../_services/user.service';
import {AlertifyService} from '../_services/alertify.service';
import {catchError} from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountEditResolver implements Resolve<UserDetailed> {

  constructor(private userService: UserService,
              private router: Router,
              private alertifyService: AlertifyService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<UserDetailed> {
    return this.userService.getUserDetailed(route.params.id).pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        this.router.navigate(['/dashboard']);
        return of(null);
      })
    );
  }

}
