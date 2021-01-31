import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {FlashcardForMarkAsHard} from '../_models/_dtos/toServer/flashcardForMarkAsHard';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {FlashcardForMarkAsIgnored} from '../_models/_dtos/toServer/flashcardForMarkAsIgnored';
import {FlashcardForClearProgress} from '../_models/_dtos/toServer/flashcardForClearProgress';
import {LessonForClearProgress} from '../_models/_dtos/toServer/lessonForClearProgress';
import {CourseForClearProgress} from '../_models/_dtos/toServer/courseForClearProgress';

@Injectable({
  providedIn: 'root'
})
export class CourseManagerService {
  private readonly baseUrl = environment.apiUrl + '/subscribedCourseManager';

  constructor(private httpClient: HttpClient) {
  }

  markFlashcardAsHard(dto: FlashcardForMarkAsHard): Observable<boolean> {
    const url = this.baseUrl + '/' + 'markFlashcardAsHard';
    return this.httpClient.post(url, dto, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return response.ok;
      })
    );
  }

  markFlashcardAsIgnored(dto: FlashcardForMarkAsIgnored): Observable<boolean> {
    const url = this.baseUrl + '/' + 'markFlashcardAsIgnored';
    return this.httpClient.post(url, dto, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return response.ok;
      })
    );
  }

  clearProgressForFlashcard(dto: FlashcardForClearProgress): Observable<boolean> {
    const url = this.baseUrl + '/' + 'clearProgressForFlashcard';
    return this.httpClient.post(url, dto, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return response.ok;
      })
    );
  }

  clearProgressForLesson(dto: LessonForClearProgress): Observable<boolean> {
    const url = this.baseUrl + '/' + 'clearProgressForLesson';
    return this.httpClient.post(url, dto, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return response.ok;
      })
    );
  }

  clearProgressForCourse(dto: CourseForClearProgress): Observable<boolean> {
    const url = this.baseUrl + '/' + 'clearProgressForCourse';
    return this.httpClient.post(url, dto, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return response.ok;
      })
    );
  }

}
