import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LessonForCheck} from '../core/_models/_dtos/fromServer/lessonForCheck';

@Component({
  selector: 'app-lesson-for-check-card',
  templateUrl: './lesson-for-check-card.component.html',
  styleUrls: ['./lesson-for-check-card.component.css']
})
export class LessonForCheckCardComponent implements OnInit {
  @Input() lessonForCheck: LessonForCheck;
  @Output() lessonSelected: EventEmitter<LessonForCheck> = new EventEmitter<LessonForCheck>();

  constructor() { }

  ngOnInit(): void {
  }

  select(): void {
    this.lessonSelected.emit(this.lessonForCheck);
  }
}
