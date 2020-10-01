import {Component, Input, OnInit} from '@angular/core';
import {SubscribedCourse} from '../../core/_models/subscribedCourse';
import {AlertifyService} from '../../core/_services/alertify.service';

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent implements OnInit {
  @Input() course: SubscribedCourse;

  constructor(private alertifyService: AlertifyService) { }

  ngOnInit(): void {
  }

  playRepetition() {
    this.alertifyService.showMessageAlert('Will be soon');
  }

  playHardFlashcards() {
    this.alertifyService.showMessageAlert('Will be soon');
  }

  playNextFlashcards() {
    this.alertifyService.showMessageAlert('Will be soon');
  }

}
