import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {FlashcardForCreate} from '../../core/_models/_dtos/toServer/flashcardForCreate';
import {FlashcardAdapter} from '../../core/_adapters/flashcardAdapter';
import {Lesson} from '../../core/_models/_dtos/fromServer/lesson';
import {Observable, Subscription} from 'rxjs';
import {Flashcard} from '../../core/_models/_dtos/fromServer/flashcard';
import {MapExtensions} from '../../core/_extensions/mapExtensions';
import {AlertifyService} from '../../core/_services/alertify.service';
import {FlashcardForUpdate} from '../../core/_models/_dtos/toServer/flashcardForUpdate';

@Component({
  selector: 'app-flashcard-form',
  templateUrl: './flashcard-form.component.html',
  styleUrls: ['./flashcard-form.component.css']
})
export class FlashcardFormComponent implements OnInit {
  private subscription: Subscription;
  @Input() lesson: Lesson;
  @Input() onFlashcardGetDetails: Observable<Flashcard>;
  @Output() addFlashcard: EventEmitter<FlashcardForCreate> = new EventEmitter<FlashcardForCreate>();
  @Output() updateFlashcard: EventEmitter<FlashcardForUpdate> = new EventEmitter<FlashcardForUpdate>();
  @Output() getFlashcardDetails: EventEmitter<number> = new EventEmitter<number>();
  flashcardForm: FormGroup;
  flashcardFormCollapsed = true;
  flashcardToUpdate = false;

  constructor(private formBuilder: FormBuilder,
              private flashcardAdapter: FlashcardAdapter,
              private mapExtensions: MapExtensions,
              private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    this.createFlashcardForm();
    // this.subscription = this.onFlashcardGetDetails
    //   .subscribe((flashcard) => this.openFlashcardFormForUpdate(flashcard), error => {
    //     console.log(error);
    //     this.alertifyService.showErrorAlert('Cannot retreive the data.');
    //   });
    console.log('Flashcard form on init...');
    console.log(this.lesson);
  }

  private openFlashcardFormForUpdate(flashcard: Flashcard): void {
    this.flashcardForm.reset();
    this.flashcardForm.patchValue({
      phrase: flashcard.phrase,
      phrasePronunciation: flashcard.phrasePronunciation,
      phraseSampleSentence: flashcard.phraseSampleSentence,
      phraseComment: flashcard.phraseComment,
      translatedPhrase: flashcard.translatedPhrase,
      translatedPhraseSampleSentence: flashcard.translatedPhraseSampleSentence,
      translatedPhraseComment: flashcard.translatedPhraseComment,
      languageLevel: this.mapExtensions.mapLanguageLevelToNumber(flashcard.languageLevel),
      category: flashcard.category
    });
    this.flashcardToUpdate = true;
    this.flashcardFormCollapsed = false;
  }

  private openFlashcardFormForCreate(): void {
    this.flashcardForm.reset();
    this.flashcardToUpdate = false;
  }

  private createFlashcardForm(): void {
    this.flashcardForm = this.formBuilder.group({
      phrase: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(64)]],
      phrasePronunciation: ['', Validators.maxLength(64)],
      phraseSampleSentence: ['', Validators.maxLength(128)],
      phraseComment: ['', Validators.maxLength(128)],
      translatedPhrase: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(64)]],
      translatedPhraseSampleSentence: ['', Validators.maxLength(128)],
      translatedPhraseComment: ['', Validators.maxLength(128)],
      languageLevel: ['', Validators.required],
      category: ['']
    });
  }

  createFlashcard(): void {
    if (this.flashcardToUpdate) {
      const flashcardForUpdate = this.flashcardAdapter.adaptFlashcardForUpdate(this.flashcardForm.value);
      this.updateFlashcard.emit(flashcardForUpdate);
    } else {
      const flashcardForCreate = this.flashcardAdapter.adaptFlashcardForCreate(this.flashcardForm.value);
      this.addFlashcard.emit(flashcardForCreate);
    }
  }

  onGetFlashcardDetails(id: number): void {
    // TODO: Check if flashcard form is collapsed, if not then close
    // Also if it is not collapsed and is dirty (someone doesn't saved flashcard)
    // Ask user if want to continue
    this.getFlashcardDetails.emit(id);
  }

}
