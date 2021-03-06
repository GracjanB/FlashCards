import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {Observable, of} from 'rxjs';
import {LearnService} from '../_services/learn.service';
import {AlertifyService} from '../_services/alertify.service';
import {catchError} from 'rxjs/operators';
import {LearnConfiguration} from '../_models/learnConfiguration';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LearnLessonResolver implements Resolve<LearnConfiguration> {

  constructor(private learnService: LearnService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<LearnConfiguration> {
    return this.learnService.getFlashcardsForLearnFromExactLesson(route.params.subCourseId, route.params.subLessonId).pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        this.router.navigate(['/dashboard']);
        return of(null);
      })
    );
  }

}
