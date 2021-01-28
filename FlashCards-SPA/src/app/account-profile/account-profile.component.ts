import { Component, OnInit } from '@angular/core';
import { UserService } from '../core/_services/user.service';
import {ActivatedRoute, Router} from '@angular/router';
import { UserDetailedWithCourses } from '../core/_models/_dtos/fromServer/userDetailedWithCourses';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';

@Component({
  selector: 'app-account-profile',
  templateUrl: './account-profile.component.html',
  styleUrls: ['./account-profile.component.css']
})
export class AccountProfileComponent implements OnInit {
  user: UserDetailedWithCourses;
  displayedCourses: CourseShort[];

  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.userDetailedWithCourses;
    });
    this.displayedCourses = this.user.subscribedCourses;
  }

  showSubscribedCourses(): void {
    this.displayedCourses = this.user.subscribedCourses;
  }

  showCreatedCourses(): void {
    this.displayedCourses = this.user.createdCourses;
  }

  editProfile(): void {
    this.router.navigate(['/account/edit/' + this.user.id]);
  }

}
