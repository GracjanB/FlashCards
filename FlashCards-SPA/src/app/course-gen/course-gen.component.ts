import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {AlertifyService} from '../core/_services/alertify.service';
import {LessonShort} from '../core/_models/_dtos/fromServer/lessonShort';
import {Flashcard} from '../core/_models/_dtos/fromServer/flashcard';
import {CourseForCreate} from '../core/_models/_dtos/toServer/courseForCreate';
import {CourseDetailed} from '../core/_models/_dtos/fromServer/courseDetailed';
import {CourseService} from '../core/_services/course.service';
import {CourseAdapter} from '../core/_adapters/courseAdapter';
import {Subject} from 'rxjs';
import {Lesson} from '../core/_models/_dtos/fromServer/lesson';
import {LessonService} from '../core/_services/lesson.service';
import {LessonForCreate} from '../core/_models/_dtos/toServer/lessonForCreate';
import {LessonAdapter} from '../core/_adapters/lessonAdapter';
import {FlashcardForCreate} from '../core/_models/_dtos/toServer/flashcardForCreate';
import {FlashcardService} from '../core/_services/flashcard.service';
import {FlashcardAdapter} from '../core/_adapters/flashcardAdapter';
import {FlashcardShort} from '../core/_models/_dtos/fromServer/flashcardShort';
import {FlashcardForUpdate} from '../core/_models/_dtos/toServer/flashcardForUpdate';
import {CourseFormComponent} from './course-form/course-form.component';
import {LessonListComponent} from './lesson-list/lesson-list.component';
import {FlashcardFormComponent} from './flashcard-form/flashcard-form.component';
import {LessonFormComponent} from './lesson-form/lesson-form.component';

@Component({
  selector: 'app-course-gen',
  templateUrl: './course-gen.component.html',
  styleUrls: ['./course-gen.component.css']
})
export class CourseGenComponent implements OnInit, AfterViewInit {
  onCourseUpdated: Subject<CourseForCreate> = new Subject<CourseForCreate>();
  onFlashcardAdd: Subject<FlashcardShort> = new Subject<FlashcardShort>();
  onFlashcardUpdated: Subject<any> = new Subject<any>();
  @ViewChild(CourseFormComponent) courseForm: CourseFormComponent;
  @ViewChild(LessonListComponent) lessonList: LessonListComponent;
  @ViewChild(FlashcardFormComponent) flashcardForm: FlashcardFormComponent;
  @ViewChild(LessonFormComponent) lessonForm: LessonFormComponent;
  course: CourseDetailed;
  lessons: LessonShort[] = [];
  selectedLesson: Lesson;
  courseSettingsActive: boolean;
  lessonsActive: boolean;
  lessonFormActive: boolean;
  flashcardsActive: boolean;
  flashcardsTabDisabled = true;

  constructor(private courseService: CourseService,
              private courseAdapter: CourseAdapter,
              private alertifyService: AlertifyService,
              private lessonService: LessonService,
              private lessonAdapter: LessonAdapter,
              private flashcardService: FlashcardService,
              private flashcardAdapter: FlashcardAdapter) {
    this.lessonsActive = false;
    this.lessonFormActive = false;
    this.flashcardsActive = false;
    this.courseSettingsActive = true;
  }

  ngOnInit(): void {
    this.navigateToCourseSettings();
  }

  ngAfterViewInit(): void {
    console.log('Course generator init...');
  }

  getCourse(): CourseForCreate {
    return this.course == null ? new CourseForCreate('', '', 0) :
      this.courseAdapter.adaptCourseForCreate(this.course);
  }

  getSelectedLesson(): Lesson {
    return this.selectedLesson;
  }

  navigateToCourseSettings(): void {
    if (this.flashcardsActive) {
      this.alertifyService.showConfirmAlert('Any unsaved changes will be lost, continue?', () => {
        this.flashcardsTabDisabled = true;
        this.toggleActiveComponents(0);
      });
    } else {
      this.toggleActiveComponents(0);
    }
  }

  navigateToLessons(): void {
    if (this.course == null) {
      this.alertifyService.showWarningAlert('Kurs nie został utworzony');
    } else {
      if (this.flashcardsActive) {
        this.alertifyService.showConfirmAlert('Any unsaved changes will be lost, continue?', () => {
          this.flashcardsTabDisabled = true;
          this.toggleActiveComponents(1);
        });
      } else {
        this.toggleActiveComponents(1);
      }
    }
  }

  navigateToLessonForm(): void {
    if (this.course == null) {
      this.alertifyService.showWarningAlert('Kurs nie został utworzony');
    } else {
      if (this.flashcardsActive) {
        this.alertifyService.showConfirmAlert('Any unsaved changes will be lost, continue?', () => {
          this.flashcardsTabDisabled = true;
          this.toggleActiveComponents(2);
        });
      } else {
        this.toggleActiveComponents(2);
      }
    }
  }

  navigateToFlashcards(): void {
    // TODO
    this.flashcardsTabDisabled = false;
    this.toggleActiveComponents(3);
  }

  private toggleActiveComponents(option: number): void {
    switch (option) {
      case 0: // Activate Course Settings
        this.lessonsActive = false;
        this.lessonFormActive = false;
        this.flashcardsActive = false;
        this.courseSettingsActive = true;
        break;

      case 1: // Activate Lessons List
        this.lessonFormActive = false;
        this.flashcardsActive = false;
        this.courseSettingsActive = false;
        this.lessonsActive = true;
        break;

      case 2: // Activate Lesson Form
        this.flashcardsActive = false;
        this.courseSettingsActive = false;
        this.lessonsActive = false;
        this.lessonFormActive = true;
        break;

      case 3: // Activate Flashcards Form
        this.courseSettingsActive = false;
        this.lessonsActive = false;
        this.lessonFormActive = false;
        this.flashcardsActive = true;
        break;
    }
  }

  openLessonForm(): void {
    this.toggleActiveComponents(2);
  }

  updateCourse(event: CourseForCreate): void {
    if (this.course == null) {
      this.courseService.createCourse(event).subscribe((courseDetailed) => {
        this.course = courseDetailed;
        const courseToSendBack = this.courseAdapter.adaptCourseForCreate(this.course);
        this.onCourseUpdated.next(courseToSendBack);
      }, error => {
        this.onCourseUpdated.error(error);
      });
    } else {
      const courseForUpdate = this.courseAdapter.adaptCourseForUpdate(event);
      this.courseService.updateCourse(this.course.id, courseForUpdate).subscribe(next => {
        this.course.name = event.name;
        this.course.description = event.description;
        this.course.courseType = event.courseType;
        const courseToSendBack = this.courseAdapter.adaptCourseForCreate(this.course);
        this.onCourseUpdated.next(courseToSendBack);
      }, error => {
        this.onCourseUpdated.thrownError(error);
      });
    }
  }

  onLessonSelected(event: LessonShort): void {
    this.lessonService.getLessonDetailed(event.courseId, event.id).subscribe((lesson) => {
      this.selectedLesson = lesson;
      this.navigateToFlashcards();
    }, error => {
      this.alertifyService.showErrorAlert('Error during load lesson');
    });
  }

  createLesson(lessonForCreate: LessonForCreate): void {
    // Zrobić tutaj tak samo jak z kursem, jeżeli nie ma lekcji to create
    // A jeżeli jest to update
    this.lessonService.createLesson(this.course.id, lessonForCreate).subscribe((lesson) => {
      this.selectedLesson = lesson;
      this.selectedLesson.flashcards = [];
      const lessonShort = this.lessonAdapter.adaptLessonShort(lesson, this.course.id);
      this.lessons.push(lessonShort);
      this.navigateToFlashcards();
    }, error => {
      this.alertifyService.showErrorAlert('Error happend. Think about better message');
    });
  }

  createFlashcard(flashcardForCreate: FlashcardForCreate): void {
    console.log(flashcardForCreate);
    this.flashcardService.createFlashcard(this.course.id, this.selectedLesson.id, flashcardForCreate)
      .subscribe((flashcard) => {
      const flashcardShort = this.flashcardAdapter.adaptFlashcardShort(flashcard);
      if (this.selectedLesson.flashcards == null) {
        this.selectedLesson.flashcards = [];
      }
      this.selectedLesson.flashcards.push(flashcardShort);
      this.onFlashcardAdd.next(flashcardShort);
    }, error => {
        console.log(error);
        this.onFlashcardAdd.error(error);
      });
  }

  updateFlashcard(flashcardForUpdate: FlashcardForUpdate): void {
    this.flashcardService.updateFlashcard(this.course.id, this.selectedLesson.id, flashcardForUpdate.id, flashcardForUpdate)
      .subscribe((flashcard) => {
      this.onFlashcardUpdated.next();
    }, error => {
        console.log(error);
        this.onFlashcardUpdated.error(error);
      });
  }

}
