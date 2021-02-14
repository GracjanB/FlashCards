import {CanActivate, Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {AlertifyService} from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService,
              private router: Router,
              private alertifyService: AlertifyService) {
  }

  canActivate(): boolean {
    if (this.authService.userIsLoggedIn()){
      return true;
    }
    this.alertifyService.showErrorAlert('Musisz się zalogować');
    this.router.navigate(['']);
  }

}
