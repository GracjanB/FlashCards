import { Lesson } from '../_models/_dtos/fromServer/lesson';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { LessonService } from '../_services/lesson.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LessonDetailResolver implements Resolve<Lesson> {

  constructor(private lessonService: LessonService,
              private router: Router,
              private alertifyService: AlertifyService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Lesson> {
    return this.lessonService.getLessonDetailed(route.params.courseId, route.params.id).pipe(
      catchError(error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Problem with retrieving data');
        const url = '/courses/' + route.params.courseId;
        this.router.navigate([url]);
        return of(null);
      })
    );
  }

}
