import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { User } from '../_models/_dtos/userInfo';
import { UserForLogin } from '../_models/_dtos/userForLogin';
import { map } from 'rxjs/operators';
import { UserForRegister } from '../_models/_dtos/userForRegister';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  currentUser: User;
  private baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  login(model: UserForLogin) {
    return this.httpClient.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUser = user.user;
        }
      })
    );
  }

  register(model: UserForRegister) {
    return this.httpClient.post(this.baseUrl + 'register', model);
  }
}
