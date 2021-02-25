import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {UserForLogin} from '../_models/_dtos/userForLogin';
import {map} from 'rxjs/operators';
import {UserForRegister} from '../_models/_dtos/userForRegister';
import {JwtHelperService} from '@auth0/angular-jwt';
import {UserInfo} from '../_models/_dtos/userInfo';
import {User} from '../_models/_dtos/fromServer/user';
import {UserDetailed} from '../_models/_dtos/fromServer/userDetailed';
import {UserRoleEnum} from '../_models/enums/userRoleEnum';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl;
  jwtHelper: JwtHelperService;

  constructor(private httpClient: HttpClient) {
    this.jwtHelper = new JwtHelperService();
  }

  login(model: UserForLogin) {
    return this.httpClient.post(this.baseUrl + 'auth/login', model).pipe(
      map((userInfo: UserInfo) => {
        localStorage.setItem('user', JSON.stringify(userInfo.user));
        localStorage.setItem('token', userInfo.token);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  register(model: UserForRegister) {
    return this.httpClient.post(this.baseUrl + 'auth/register', model);
  }

  registerAdministrator(model: UserForRegister): Observable<boolean> {
    const url = this.baseUrl + 'auth/register/admin';
    return this.httpClient.post(url, model, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(map(response => {
      return response.ok;
    }));
  }

  userIsLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  getLoggedInUserInfo(): UserDetailed {
    return JSON.parse(localStorage.getItem('user')) as UserDetailed;
  }

  userIsAdministrator(): boolean {
    const user = JSON.parse(localStorage.getItem('user')) as User;
    return user.role === UserRoleEnum.Administrator;
  }

  userIsSuperAdministrator(): boolean {
    const user = JSON.parse(localStorage.getItem('user')) as User;
    return user.role === UserRoleEnum.SuperAdministrator;
  }
}
