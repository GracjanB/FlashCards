import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CourseParams } from '../_models/_dtos/toServer/courseParams';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/common/pagination';
import { Course } from '../_models/_dtos/fromServer/course';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { CourseAdapter } from '../_adapters/courseAdapter';
import { CourseForUpdate } from '../_models/_dtos/toServer/courseForUpdate';
import { CourseForCreate } from '../_models/_dtos/toServer/courseForCreate';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  baseUrl = environment.apiUrl + 'courses';

  constructor(private httpClient: HttpClient,
              private adapter: CourseAdapter) { }

  getCourses(page: number, itemsPerPage: number, courseParams: CourseParams): Observable<PaginatedResult<Course[]>> {
    const paginatedResult: PaginatedResult<any> = new PaginatedResult<any>();
    let params = new HttpParams();
    params = params.append('pageNumber', page.toString());
    params = params.append('pageSize', itemsPerPage.toString());
    params = params.append('courseType', courseParams.courseType.toString());

    return this.httpClient.get(this.baseUrl, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response',
      params: params
    }).pipe(map(response => {
      const coursesFromBody = response.body as [];
      const courses: Course[] = [];
      for (const course of coursesFromBody) {
        courses.push(this.adapter.adaptCourse(course));
      }
      paginatedResult.result = courses;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    }));
  }

  getCourse(id: number): Observable<Course> {
    const url = this.baseUrl + '/' + id;
    return this.httpClient.get(url, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.adapter.adaptCourse(response.body);
      })
    );
  }

  updateCourse(id: number, model: CourseForUpdate) {
    return this.httpClient.put(this.baseUrl + '/' + id, model, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      })
    });
  }

  createCourse(model: CourseForCreate) {
    return this.httpClient.post(this.baseUrl, model, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      })
    });
  }
}
