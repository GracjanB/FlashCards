import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {SubscribedCourseShort} from '../../core/_models/subscribedCourseShort';
import {AlertifyService} from '../../core/_services/alertify.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-sub-course-card',
  templateUrl: './sub-course-card.component.html',
  styleUrls: ['./sub-course-card.component.css']
})
export class SubCourseCardComponent implements OnInit {
  @Input() course: SubscribedCourseShort;
  @Output() unsubscribeCourse: EventEmitter<number> = new EventEmitter<number>();

  constructor(private alertifyService: AlertifyService,
              private router: Router) { }

  ngOnInit(): void {
  }

  playRepetition() {
    this.router.navigate(['repetition/course/' + this.course.subscriptionId]);
  }

  playHardFlashcards() {
    this.alertifyService.showMessageAlert('Will be soon');
  }

  playNextFlashcards() {
    this.router.navigate(['learn/course/' + this.course.subscriptionId]);
  }

  onUnsubscribeCourse(): void {
    this.unsubscribeCourse.emit(this.course.subscriptionId);
  }

  navigateToDetails(): void {
    this.router.navigate(['/courses/' + this.course.courseId]);
  }
}
