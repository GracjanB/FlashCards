import {Component, OnInit} from '@angular/core';
import {AuthService} from '../core/_services/auth.service';
import {Router} from '@angular/router';
import {User} from '../core/_models/_dtos/fromServer/user';

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

  navigateToAccountProfile(): void {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.router.navigate(['/account/profile/' + user.id]);
  }

  navigateToAccountSettings(): void {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.router.navigate(['/account/edit/' + user.id]);
  }

  userIsLoggedIn(): boolean {
    if (this.authService.userIsLoggedIn()) {
      const user: User = JSON.parse(localStorage.getItem('user'));
      this.userDisplayName = user.displayName;
      return true;
    }
    return false;
  }

  userIsAdministrator(): boolean {
    return this.authService.userIsAdministrator() || this.authService.userIsSuperAdministrator();
  }

  navigateToAdministratorPanel(): void {
    this.router.navigate(['/admin/panel']);
  }
}
