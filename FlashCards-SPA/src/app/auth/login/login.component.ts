import { Component, OnInit } from '@angular/core';
import { UserForLogin } from '../../core/_models/_dtos/userForLogin';
import { AuthService } from '../../core/_services/auth.service';
import {AlertifyService} from '../../core/_services/alertify.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userForLogin: UserForLogin;
  isBusy = false;

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService) {
    this.userForLogin = new UserForLogin();
  }

  ngOnInit(): void {
  }

  login() {
    this.isBusy = true;
    this.authService.login(this.userForLogin).subscribe(
      next => {
      this.alertifyService.showSuccessAlert('Successfully logged in');
    }, error => {
      this.alertifyService.showErrorAlert('Failed to login');
    }, () => {
      this.isBusy = false;
    });
  }

}
