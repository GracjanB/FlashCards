import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {CourseAdapter} from '../_adapters/courseAdapter';
import {Observable} from 'rxjs';
import {CourseForCheck} from '../_models/_dtos/fromServer/courseForCheck';
import {environment} from '../../../environments/environment';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdministrationService {
  private baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient,
              private courseAdapter: CourseAdapter) {
  }

  getCoursesForCheck(): Observable<Array<CourseForCheck>> {
    const url = this.baseUrl + 'courses/admin/forCheck';
    return this.httpClient.get(url, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      const coursesFromBody = response.body as [];
      const courses = new Array<CourseForCheck>();
      for (const course of coursesFromBody) {
        courses.push(this.courseAdapter.adaptCourseForCheck(course));
      }
      return courses;
    }));
  }

  getCourseForCheck(courseId: number): Observable<CourseForCheck> {
    const url = this.baseUrl + 'courses/' + courseId + '/admin/forCheck';
    return this.httpClient.get(url, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      return this.courseAdapter.adaptCourseForCheck(response.body);
    }));
  }

  acceptCourse(courseId: number): Observable<boolean> {
    const url = this.baseUrl + 'courses/' + courseId + '/admin/accept';
    return this.httpClient.patch(url, null, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      return response.ok;
    }));
  }

  blockCourse(courseId: number): Observable<boolean> {
    const url = this.baseUrl + 'courses/' + courseId + '/admin/block';
    return this.httpClient.patch(url, null, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      return response.ok;
    }));
  }

  // Dorobić ten endpoint
  notAcceptCourse(courseId: number): Observable<boolean> {
    const url = this.baseUrl + 'courses/' + courseId + '/admin/notAccept';
    return this.httpClient.patch(url, null, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      return response.ok;
    }));
  }
}
