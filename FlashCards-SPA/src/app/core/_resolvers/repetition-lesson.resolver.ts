import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {Observable, of} from 'rxjs';
import {LearnConfiguration} from '../_models/learnConfiguration';
import {LearnService} from '../_services/learn.service';
import {AlertifyService} from '../_services/alertify.service';
import {catchError} from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RepetitionLessonResolver implements Resolve<LearnConfiguration> {

  constructor(private learnService: LearnService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<LearnConfiguration> {
    return this.learnService.getFlashcardsForRepetitionFromExactLesson(route.params.subCourseId, route.params.subLessonId).pipe(
      catchError(error => {
        if (error.status === 404) {
          this.alertifyService.showWarningAlert('Brak słówek do powtórki');
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
