import {Component, OnInit} from '@angular/core';
import {CourseShort} from '../core/_models/_dtos/fromServer/courseShort';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseDetailed} from '../core/_models/_dtos/fromServer/courseDetailed';
import {LessonShort} from '../core/_models/_dtos/fromServer/lessonShort';
import {SubscribedCourseDetail} from '../core/_models/_dtos/fromServer/subscribedCourseDetail';
import {SubscriptionsService} from '../core/_services/subscriptions.service';
import {AlertifyService} from '../core/_services/alertify.service';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  // Course may be instance of SubscribedCourseDetail or CourseDetailed
  // depending on subscription by user
  course: any;
  isSubscribed: boolean;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private subscriptionsService: SubscriptionsService,
              private alertifyService: AlertifyService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.course = data.course;
      this.prepareComponent();
    });
  }

  onLessonSelected(lesson: any): void {
    let url = '';
    if (this.course instanceof CourseDetailed) {
      url = 'courses/' + this.course.id + '/lessons/' + lesson.id;
    } else {
      url = 'courses/' + this.course.courseId + '/lessons/' + lesson.id;
    }
    this.router.navigate([url]);
  }

  onLearnContinue(): void {
    const subCourseId = this.course.subscriptionId;
    this.router.navigate(['learn/course/' + subCourseId]);
  }

  private prepareComponent(): void {
    this.isSubscribed = this.course instanceof SubscribedCourseDetail;
  }

  public onSubscribeCourse(): void {
    this.subscriptionsService.subscribeCourse(this.course.id).subscribe(next => {
      window.location.reload();
    }, error => {
      this.alertifyService.showErrorAlert('An error occurred. Try again later..');
    });
  }

  public onUnsubscribeCourse(): void {
    this.alertifyService.showConfirmAlert('Czy na pewno chcesz odsubskrybować kurs? Tej operacji nie można cofnąć.',
      () => {
        this.subscriptionsService.unsubscribeCourse(this.course.subscriptionId).subscribe(result => {
          if (result) {
            window.location.reload();
          } else {
            this.alertifyService.showErrorAlert('An error occurred. Try again later..');
          }
        }, error => {
          this.alertifyService.showErrorAlert('An error occurred. Try again later..');
        });
      });
  }
}
