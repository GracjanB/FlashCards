import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {UserForLogin} from '../_models/_dtos/userForLogin';
import {map} from 'rxjs/operators';
import {UserForRegister} from '../_models/_dtos/userForRegister';
import {JwtHelperService} from '@auth0/angular-jwt';
import {UserInfo} from '../_models/_dtos/userInfo';
import {User} from '../_models/_dtos/fromServer/user';
import {UserDetailed} from '../_models/_dtos/fromServer/userDetailed';

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

  userIsLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  getLoggedInUserInfo(): UserDetailed {
    return JSON.parse(localStorage.getItem('user')) as UserDetailed;
  }
}
