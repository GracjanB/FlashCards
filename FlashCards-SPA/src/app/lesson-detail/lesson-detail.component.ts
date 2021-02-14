import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {Lesson} from '../core/_models/_dtos/fromServer/lesson';
import {CourseManagerService} from '../core/_services/courseManager.service';
import {FlashcardShort} from '../core/_models/_dtos/fromServer/flashcardShort';
import {AuthService} from '../core/_services/auth.service';
import {FlashcardForMarkAsHard} from '../core/_models/_dtos/toServer/flashcardForMarkAsHard';
import {AlertifyService} from '../core/_services/alertify.service';
import {FlashcardForMarkAsIgnored} from '../core/_models/_dtos/toServer/flashcardForMarkAsIgnored';

@Component({
  selector: 'app-lesson-detail',
  templateUrl: './lesson-detail.component.html',
  styleUrls: ['./lesson-detail.component.css']
})
export class LessonDetailComponent implements OnInit {
  lessonDetailed: Lesson;
  isSubscribed: boolean;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private courseManagerService: CourseManagerService,
              private authService: AuthService,
              private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.lessonDetailed = data.lesson;
      this.prepareComponent();
    });
  }

  private prepareComponent(): void {
    this.isSubscribed = this.lessonDetailed.isSubscribed;
  }

  markFlashcardAsHard(flashcard: FlashcardShort): void {
    const accountId = this.authService.getLoggedInUserInfo().accountId;
    const dto = new FlashcardForMarkAsHard(flashcard.subscribedFlashcardId, accountId, !flashcard.markedAsHard);
    this.courseManagerService.markFlashcardAsHard(dto).subscribe(result => {
      if (result) {
        flashcard.markedAsHard = !flashcard.markedAsHard;
        this.alertifyService.showSuccessAlert('Operacja wykonana pomyślnie');
      } else {
        this.alertifyService.showErrorAlert('Wystąpił błąd');
      }
    }, error => {
      this.alertifyService.showErrorAlert('Wystąpił błąd');
    });
  }

  markFlashcardAsIgnored(flashcard: FlashcardShort): void {
    const accountId = this.authService.getLoggedInUserInfo().accountId;
    const dto = new FlashcardForMarkAsIgnored(flashcard.subscribedFlashcardId, accountId, !flashcard.markedAsIgnored);
    this.courseManagerService.markFlashcardAsIgnored(dto).subscribe(result => {
      if (result) {
        flashcard.markedAsHard = !flashcard.markedAsHard;
        this.alertifyService.showSuccessAlert('Operacja wykonana pomyślnie');
      } else {
        this.alertifyService.showErrorAlert('Wystąpił błąd');
      }
    }, error => {
      this.alertifyService.showErrorAlert('Wystąpił błąd');
    });
  }

  countOverallLessonProgress(): number {
    const totalScore = this.lessonDetailed.flashcards.length * 10;
    let alreadyLearnedScore = 0;
    this.lessonDetailed.flashcards.forEach(flashcard => alreadyLearnedScore += flashcard.progress);
    return (alreadyLearnedScore / totalScore) * 100;
  }
}
