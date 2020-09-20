import {Component, OnInit} from '@angular/core';
import {AuthService} from '../core/_services/auth.service';
import {Router} from '@angular/router';
import {User} from '../core/_models/user';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  isCollapsed: boolean;
  userDisplayName = '';

  constructor(private authService: AuthService,
              private router: Router) {}

  ngOnInit(): void {
    this.isCollapsed = false;
  }

  logout(): void {
    this.authService.logout();
    this.userDisplayName = '';
    this.router.navigate(['']);
  }

  userIsLoggedIn(): boolean {
    if (this.authService.userIsLoggedIn()) {
      const user: User = JSON.parse(localStorage.getItem('user'));
      this.userDisplayName = user.displayName;
      return true;
    }
    return false;
  }

}
