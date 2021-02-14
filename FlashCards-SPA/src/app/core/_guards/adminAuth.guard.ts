import {CanActivate, Router} from '@angular/router';
import {AuthService} from '../_services/auth.service';
import {AlertifyService} from '../_services/alertify.service';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuard implements CanActivate {

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService,
              private router: Router) {
  }

  canActivate(): boolean {
    if (this.authService.userIsLoggedIn() &&
      (this.authService.userIsAdministrator() ||
        this.authService.userIsSuperAdministrator())) {
      return true;
    } else {
      this.alertifyService.showErrorAlert('Brak uprawnień');
      if (this.authService.userIsLoggedIn()) {
        this.router.navigate(['/dashboard']);
      } else {
        this.router.navigate(['']);
      }
    }
  }
}
