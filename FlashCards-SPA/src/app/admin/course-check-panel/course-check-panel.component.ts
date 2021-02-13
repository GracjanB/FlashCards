import { Component, OnInit } from '@angular/core';
import {CourseForCheck} from '../../core/_models/_dtos/fromServer/courseForCheck';
import {LessonForCheck} from '../../core/_models/_dtos/fromServer/lessonForCheck';
import {AuthService} from '../../core/_services/auth.service';
import {AlertifyService} from '../../core/_services/alertify.service';
import {ActivatedRoute, Router} from '@angular/router';
import {AdministrationService} from '../../core/_services/administration.service';

@Component({
  selector: 'app-course-check-panel',
  templateUrl: './course-check-panel.component.html',
  styleUrls: ['./course-check-panel.component.css']
})
export class CourseCheckPanelComponent implements OnInit {
  courseForCheck: CourseForCheck;
  selectedLesson: LessonForCheck;

  constructor(private adminService: AdministrationService,
              private alertifyService: AlertifyService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.courseForCheck = data.courseForCheck;
    });
  }

  onLessonSelected(lesson: LessonForCheck): void {
    this.selectedLesson = lesson;
  }

  acceptCourse(): void {
    this.alertifyService.showConfirmAlert('Czy na pewno chcesz zaakceptować kurs?', () => {
      this.adminService.acceptCourse(this.courseForCheck.id).subscribe(next => {
        this.alertifyService.showSuccessAlert('Status kursu został zmieniony');
        this.router.navigate(['/admin/panel']);
      }, error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Wystąpił błąd. Spróbuj ponownie');
      });
    });
  }

  notAcceptCourse(): void {
    this.alertifyService.showConfirmAlert('Czy na pewno nie chcesz zaakceptować kursu?', () => {
      this.adminService.notAcceptCourse(this.courseForCheck.id).subscribe(next => {
        this.alertifyService.showSuccessAlert('Status kursu został zmieniony');
        this.router.navigate(['/admin/panel']);
      }, error => {
        console.log(error);
        this.alertifyService.showErrorAlert('Wystąpił błąd. Spróbuj ponownie');
      });
    });
  }
}
