import { Component, OnInit } from '@angular/core';
import { UserForLogin } from '../../core/_models/_dtos/userForLogin';
import { AuthService } from '../../core/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userForLogin: UserForLogin = new UserForLogin();

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
  }

  login() {
    console.log(this.userForLogin);
  }

}
