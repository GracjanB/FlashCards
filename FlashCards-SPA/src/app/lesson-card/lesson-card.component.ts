import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LessonShort} from '../core/_models/_dtos/fromServer/lessonShort';
import {Router} from '@angular/router';

@Component({
  selector: 'app-lesson-card',
  templateUrl: './lesson-card.component.html',
  styleUrls: ['./lesson-card.component.css']
})
export class LessonCardComponent implements OnInit {
  @Input() lesson: LessonShort;
  @Output() lessonSelected: EventEmitter<LessonShort> = new EventEmitter<LessonShort>();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onLessonClick(): void {
    // const url = 'courses/' + this.lesson.courseId + /lessons/ + this.lesson.id;
    // this.router.navigate([url]);
    this.lessonSelected.emit(this.lesson);
  }

}
