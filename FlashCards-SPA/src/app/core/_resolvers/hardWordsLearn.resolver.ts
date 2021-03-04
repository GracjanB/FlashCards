import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {LearnConfiguration} from '../_models/learnConfiguration';
import {LearnService} from '../_services/learn.service';
import {AlertifyService} from '../_services/alertify.service';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HardWordsLearnResolver implements Resolve<LearnConfiguration> {

  constructor(private learnService: LearnService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<LearnConfiguration> {
    return this.learnService.getHardWordsForLearn(route.params.subLessonId).pipe(
      catchError(error => {
        if (error.status === 404) {
          this.alertifyService.showWarningAlert('Brak słówek oznaczonych jako trudne.');
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
