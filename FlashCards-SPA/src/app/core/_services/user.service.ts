import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UserAdapter} from '../_adapters/userAdapter';
import {Observable, ObservableLike} from 'rxjs';
import {UserDetailedWithCourses} from '../_models/_dtos/fromServer/userDetailedWithCourses';
import {map} from 'rxjs/operators';
import {UserDetailed} from '../_models/_dtos/fromServer/userDetailed';
import {UserForPasswordChange} from '../_models/_dtos/toServer/userForPasswordChange';
import {UserForUpdate} from '../_models/_dtos/toServer/userForUpdate';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'users/';

  constructor(private httpClient: HttpClient,
              private userAdapter: UserAdapter) {

  }

  getUserDetailedWithCourse(userId: number): Observable<UserDetailedWithCourses> {
    const url = this.baseUrl + userId + '/courses';
    return this.httpClient.get(url, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.userAdapter.adaptUserForDetailWithCourses(response.body);
      })
    );
  }

  getUserDetailed(userId: number): Observable<UserDetailed> {
    const url = this.baseUrl + userId;
    return this.httpClient.get(url, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.userAdapter.adaptUserDetailed(response.body);
      })
    );
  }

  changePassword(userId: number, userForPasswordChange: UserForPasswordChange) {
    const url = this.baseUrl + userId + '/changePassword';
    return this.httpClient.put(url, userForPasswordChange, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      })
    });
  }

  updateUser(userId: number, userForUpdate: UserForUpdate): Observable<UserDetailed> {
    const url = this.baseUrl + userId;
    return this.httpClient.put(url, userForUpdate, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.userAdapter.adaptUserDetailed(response.body);
      })
    );
  }

}
