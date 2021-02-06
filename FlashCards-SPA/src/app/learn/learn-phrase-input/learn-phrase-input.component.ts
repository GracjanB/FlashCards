import {Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {AlertifyService} from '../../core/_services/alertify.service';
import {FlashcardLearnForInput} from '../../core/_models/flashcardLearnForInput';
import {Observable, Subscription} from 'rxjs';
import {Flashcard} from '../../core/_models/_dtos/fromServer/flashcard';

@Component({
  selector: 'app-learn-phrase-input',
  templateUrl: './learn-phrase-input.component.html',
  styleUrls: ['./learn-phrase-input.component.css']
})
export class LearnPhraseInputComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription;
  @Input() currentPhrase: FlashcardLearnForInput;
  @Input() onCanContinue: Observable<any>;
  @Output() guessResult: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() showNext: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('phraseInput') phraseInput: ElementRef;
  hintCollapsed: boolean;
  phraseGuessed: boolean;
  phraseGuessedSoFar: boolean;
  canContinue = false;

  constructor(private alertifyService: AlertifyService) {
    this.hintCollapsed = true;
    this.phraseGuessed = false;
  }

  ngOnInit(): void {
    this.subscriptions = this.onCanContinue.subscribe(next => {
      this.canContinue = true;
    }, error =>  {
      // TODO
      console.log('An error occurred');
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  next(): void {
    this.hintCollapsed = true;
    this.phraseGuessed = false;
    this.phraseGuessedSoFar = false;
    this.phraseInput.nativeElement.value = '';
    this.canContinue = false;
    this.showNext.emit();
  }

  onInputTextChanged(inputPhrase: string): void {
    this.phraseGuessedSoFar = this.currentPhrase.translatedPhrase.startsWith(inputPhrase);
    this.phraseGuessed = inputPhrase === this.currentPhrase.translatedPhrase;
    if (this.phraseGuessed) {
      this.guessResult.emit(true);
    }
  }

  showHint(): void {
    this.hintCollapsed = !this.hintCollapsed;
    this.guessResult.emit(false);
  }
}
