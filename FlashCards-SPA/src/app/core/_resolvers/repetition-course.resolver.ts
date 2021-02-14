import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {Observable, of} from 'rxjs';
import {LearnService} from '../_services/learn.service';
import {AlertifyService} from '../_services/alertify.service';
import {LearnConfiguration} from '../_models/learnConfiguration';
import {catchError} from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RepetitionCourseResolver implements Resolve<LearnConfiguration> {

  constructor(private learnService: LearnService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<LearnConfiguration> {
    return this.learnService.getFlashcardsForRepetition(route.params.subCourseId).pipe(
      catchError(error => {
        if (error.status === 404) {
          this.alertifyService.showErrorAlert(error.error);
        } else {
          this.alertifyService.showErrorAlert('Problem with retrieving data');
        }
        console.log(error);
        this.router.navigate(['/dashboard']);
        return of(null);
      })
    );
  }
}
