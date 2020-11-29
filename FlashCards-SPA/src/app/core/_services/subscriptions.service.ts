import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs';
import {PaginatedResult} from '../_models/common/pagination';
import {SubscribedCourseShort} from '../_models/subscribedCourseShort';
import {map} from 'rxjs/operators';
import {SubscriptionsAdapter} from '../_adapters/subscriptionsAdapter';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionsService {
  baseUrl = environment.apiUrl + 'subscriptions/';

  constructor(private httpClient: HttpClient,
              private subscriptionsAdapter: SubscriptionsAdapter) {
  }

  getSubscribedCourses(page: number, itemsPerPage: number): Observable<PaginatedResult<SubscribedCourseShort[]>> {
    const paginatedResult: PaginatedResult<any> = new PaginatedResult<any>();
    let params = new HttpParams();
    params = params.append('pageNumber', page.toString());
    params = params.append('pageSize', itemsPerPage.toString());

    return this.httpClient.get(this.baseUrl, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response',
      params: params
    }).pipe(map(response => {
      const subscribedCoursesFromBody = response.body as [];
      const subscribedCoursesShort: SubscribedCourseShort[] = [];
      for (const subscribedCourse of subscribedCoursesFromBody) {
        subscribedCoursesShort.push(this.subscriptionsAdapter.adaptSubscribedCourseShort(subscribedCourse));
      }
      paginatedResult.result = subscribedCoursesShort;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    }));
  }

  subscribeCourse(courseId: number): Observable<SubscribedCourseShort> {
    const url = this.baseUrl + 'subscribe/' + courseId;
    return this.httpClient.post(url, null, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      return this.subscriptionsAdapter.adaptSubscribedCourseShort(response.body);
    }));
  }

  unsubscribeCourse(subscriptionId: number): Observable<boolean> {
    const url = this.baseUrl + 'unsubscribe/' + subscriptionId;
    return this.httpClient.put(url, null, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      return response.ok;
    }));
  }
}
