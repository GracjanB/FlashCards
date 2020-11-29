import { Component, OnInit } from '@angular/core';
import {User} from '../core/_models/user';
import {SubscribedCourseShort} from '../core/_models/subscribedCourseShort';
import {SubscriptionsService} from '../core/_services/subscriptions.service';
import {Pagination} from '../core/_models/common/pagination';
import {AlertifyService} from '../core/_services/alertify.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
   currentUser: User;
   subscribedCourses: SubscribedCourseShort[];
   pagination: Pagination;

  constructor(private subscriptionsService: SubscriptionsService,
              private alertifyService: AlertifyService,
              private route: ActivatedRoute,
              private router: Router) {
    this.currentUser = JSON.parse(localStorage.getItem('user')) as User;
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.subscribedCourses = data.subscribedCourses.result;
      this.pagination = data.subscribedCourses.pagination;
    });
  }

  pageChanged(pagination: any): void {
    console.log(pagination);
  }

  public navigateToCourseGen(): void {
    this.router.navigate(['course-generator']);
  }

  public navigateToAnalytics(): void {
    this.router.navigate(['analytics']);
  }

  public unsubscribeCourse(subscriptionId: number): void {
    this.alertifyService.showConfirmAlert('Czy na pewno chcesz odsubskrybować kurs? Tej operacji nie można cofnąć.',
      () => {
        this.subscriptionsService.unsubscribeCourse(subscriptionId).subscribe(result => {
          if (result) {
            window.location.reload();
          } else {
            this.alertifyService.showErrorAlert('An error occurred during operation. Try again later..');
          }
        }, error => {
          this.alertifyService.showErrorAlert('An error occurred during operation. Try again later..');
        });
      });
  }
}
