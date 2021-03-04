import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../core/_services/auth.service';
import {CourseForCheck} from '../../core/_models/_dtos/fromServer/courseForCheck';
import {AlertifyService} from '../../core/_services/alertify.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ArgumentOutOfRangeError} from 'rxjs';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  coursesForCheck: Array<CourseForCheck>;
  coursesToCheckComponentActive: boolean;
  registerAdminComponentActive: boolean;

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.coursesForCheck = data.coursesForCheck;
    });
    this.toggleActiveComponent(0);
  }

  isAdministrator(): boolean {
    return this.authService.userIsAdministrator();
  }

  isSuperAdministrator(): boolean {
    return this.authService.userIsSuperAdministrator();
  }

  navigateToCourseCheck(courseId: number): void {
    this.router.navigate(['admin/courseCheck/' + courseId]);
  }

  toggleActiveComponent(option: number): void {
    switch (option) {
      case 0:
        this.registerAdminComponentActive = false;
        this.coursesToCheckComponentActive = true;
        break;
      case 1:
        this.coursesToCheckComponentActive = false;
        this.registerAdminComponentActive = true;
        break;
      default:
        console.log('Wrong value for toggle parameter. Actual value: ' + option);
        throw new ArgumentOutOfRangeError();
    }
  }
}
