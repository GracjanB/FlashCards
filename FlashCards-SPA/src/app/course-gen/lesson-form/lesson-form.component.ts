import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LessonAdapter} from '../../core/_adapters/lessonAdapter';
import {LessonForCreate} from '../../core/_models/_dtos/toServer/lessonForCreate';

@Component({
  selector: 'app-lesson-form',
  templateUrl: './lesson-form.component.html',
  styleUrls: ['./lesson-form.component.css']
})
export class LessonFormComponent implements OnInit {
  lessonForm: FormGroup;
  @Output() addLesson: EventEmitter<LessonForCreate> = new EventEmitter<LessonForCreate>();

  constructor(private formBuilder: FormBuilder,
              private lessonAdapter: LessonAdapter) { }

  ngOnInit(): void {
    this.createLessonForm();
  }

  private createLessonForm(): void {
    this.lessonForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.maxLength(1024)],
      category: ['', Validators.required]
    });
  }

  createLesson(): void {
    const lessonForCreate = this.lessonAdapter.adaptLessonForCreate(this.lessonForm.value);
    this.addLesson.emit(lessonForCreate);
  }

}
