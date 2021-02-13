import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CourseForCheck} from '../core/_models/_dtos/fromServer/courseForCheck';

@Component({
  selector: 'app-course-for-check-card',
  templateUrl: './course-for-check-card.component.html',
  styleUrls: ['./course-for-check-card.component.css']
})
export class CourseForCheckCardComponent implements OnInit {
  @Input() courseForCheck: CourseForCheck;
  @Output() courseSelected: EventEmitter<number> = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  onCourseClick() {
    this.courseSelected.emit(this.courseForCheck.id);
  }
}
