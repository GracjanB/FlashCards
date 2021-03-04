import {Component, OnInit} from '@angular/core';
import {UserService} from '../core/_services/user.service';
import {ActivatedRoute, Router} from '@angular/router';
import {UserDetailedWithCourses} from '../core/_models/_dtos/fromServer/userDetailedWithCourses';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {CourseTypeEnum} from '../core/_models/enums/courseTypeEnum';

@Component({
  selector: 'app-account-profile',
  templateUrl: './account-profile.component.html',
  styleUrls: ['./account-profile.component.css']
})
export class AccountProfileComponent implements OnInit {
  user: UserDetailedWithCourses;
  displayedCourses: CourseShort[];
  courseTypes = CourseTypeEnum;
  courseTypeSelected: CourseTypeEnum;

  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.userDetailedWithCourses;
    });
    this.displayedCourses = this.user.subscribedCourses;
    this.courseTypeSelected = CourseTypeEnum.Subscribed;
  }

  showSubscribedCourses(): void {
    this.displayedCourses = this.user.subscribedCourses;
    this.courseTypeSelected = CourseTypeEnum.Subscribed;
  }

  showCreatedCourses(): void {
    this.displayedCourses = this.user.createdCourses;
    this.courseTypeSelected = CourseTypeEnum.Created;
  }

  showPrivateCourses(): void {
    this.displayedCourses = this.user.privateCourses;
    this.courseTypeSelected = CourseTypeEnum.Private;
  }

  showDraftCourses(): void {
    this.displayedCourses = this.user.draftCourses;
    this.courseTypeSelected = CourseTypeEnum.Draft;
  }

  editProfile(): void {
    this.router.navigate(['/account/edit/' + this.user.id]);
  }

}
