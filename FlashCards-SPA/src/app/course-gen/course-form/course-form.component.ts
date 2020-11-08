import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CourseForCreate} from '../../core/_models/_dtos/toServer/courseForCreate';
import {CourseAdapter} from '../../core/_adapters/courseAdapter';
import {Observable, Subscription} from 'rxjs';
import {AlertifyService} from '../../core/_services/alertify.service';

@Component({
  selector: 'app-course-form',
  templateUrl: './course-form.component.html',
  styleUrls: ['./course-form.component.css']
})
export class CourseFormComponent implements OnInit {
  private eventsSubscription: Subscription;
  @Input() course: CourseForCreate;
  @Input() onCourseUpdated: Observable<CourseForCreate>;
  @Output() courseUpdate: EventEmitter<CourseForCreate> = new EventEmitter<CourseForCreate>();
  courseForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private courseAdapter: CourseAdapter,
              private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    this.createCourseForm();
    this.eventsSubscription = this.onCourseUpdated.subscribe(
      (data) => this.courseUpdated(data),
      (error) => {
        this.alertifyService.showErrorAlert(error.error);
    });
  }

  private courseUpdated(course: CourseForCreate): void {
    this.course = course;
    this.alertifyService.showSuccessAlert('Zapisano');
  }

  private createCourseForm() {
    this.courseForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
      description: ['', Validators.maxLength(1024)],
      courseType: ['', Validators.required]
    });
    if (this.course != null) {
      this.courseForm.get('name').setValue(this.course.name);
      this.courseForm.get('description').setValue(this.course.description);
      this.courseForm.get('courseType').setValue(this.course.courseType);
    }
  }

  createCourse(): void {
    const courseForCreate = this.courseAdapter.adaptCourseForCreate(this.courseForm.value);
    this.courseUpdate.emit(courseForCreate);
  }

}
