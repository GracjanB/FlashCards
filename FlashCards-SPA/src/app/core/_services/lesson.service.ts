import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LessonAdapter } from '../_adapters/lessonAdapter';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import {Observable} from 'rxjs';
import {Lesson} from '../_models/_dtos/fromServer/lesson';
import {LessonForCreate} from '../_models/_dtos/toServer/lessonForCreate';

@Injectable({
  providedIn: 'root'
})
export class LessonService {
  baseUrl = environment.apiUrl + 'courses/';

  constructor(private httpClient: HttpClient,
              private lessonAdapter: LessonAdapter) { }

  getLessonDetailed(courseId: number, lessonId: number): Observable<Lesson> {
    const url = this.baseUrl + courseId + '/lessons/' + lessonId;
    return this.httpClient.get(url, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map (response => {
        return this.lessonAdapter.adaptLesson(response.body);
      })
    );
  }

  createLesson(courseId: number, lessonForCreate: LessonForCreate): Observable<Lesson> {
    const url = this.baseUrl + courseId + '/lessons';
    return this.httpClient.post(url, lessonForCreate, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.lessonAdapter.adaptLesson(response.body);
      })
    );
  }
}
