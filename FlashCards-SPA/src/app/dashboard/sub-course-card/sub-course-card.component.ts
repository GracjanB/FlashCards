import {Component, Input, OnInit} from '@angular/core';
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

  constructor(private alertifyService: AlertifyService,
              private router: Router) { }

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
