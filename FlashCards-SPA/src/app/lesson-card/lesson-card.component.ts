import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LessonShort} from '../core/_models/_dtos/fromServer/lessonShort';
import {Router} from '@angular/router';
import {SubscribedLessonShort} from '../core/_models/_dtos/fromServer/subscribedLessonShort';

@Component({
  selector: 'app-lesson-card',
  templateUrl: './lesson-card.component.html',
  styleUrls: ['./lesson-card.component.css']
})
export class LessonCardComponent implements OnInit {
  @Input() lesson: any;
  @Output() lessonSelected: EventEmitter<any> = new EventEmitter<any>();
  isSubscribed: boolean;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.isSubscribed = this.lesson instanceof SubscribedLessonShort;
  }

  onLessonClick(): void {
    // const url = 'courses/' + this.lesson.courseId + /lessons/ + this.lesson.id;
    // this.router.navigate([url]);
    this.lessonSelected.emit(this.lesson);
  }

}
