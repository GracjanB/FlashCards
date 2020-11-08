import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LessonShort} from '../../core/_models/_dtos/fromServer/lessonShort';

@Component({
  selector: 'app-lesson-list',
  templateUrl: './lesson-list.component.html',
  styleUrls: ['./lesson-list.component.css']
})
export class LessonListComponent implements OnInit {
  @Input() lessons: LessonShort[];
  @Output() lessonClick: EventEmitter<LessonShort> = new EventEmitter<LessonShort>();
  @Output() addLesson: EventEmitter<any> = new EventEmitter<any>();

  constructor() { }

  ngOnInit(): void {
  }

  onLessonClick(event: LessonShort): void {
    this.lessonClick.emit(event);
  }

  onAddNewLesson(): void {
    this.addLesson.emit();
  }
}
