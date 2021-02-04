import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Observable, Subscription} from 'rxjs';
import {FlashcardLearnForInput} from '../../core/_models/flashcardLearnForInput';
import {FlashcardLearnForSelection} from '../../core/_models/flashcardLearnForSelection';

@Component({
  selector: 'app-learn-phrase-selection',
  templateUrl: './learn-phrase-selection.component.html',
  styleUrls: ['./learn-phrase-selection.component.css', './checkAnimation.scss']
})
export class LearnPhraseSelectionComponent implements OnInit {
  private subscriptions: Subscription;
  @Input() currentPhrase: FlashcardLearnForSelection;
  @Input() onCanContinue: Observable<any>;
  @Output() guessResult: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() showNext: EventEmitter<any> = new EventEmitter<any>();
  hintCollapsed: boolean;
  canContinue = false;

  constructor() {
    this.hintCollapsed = true;
  }

  ngOnInit(): void {
    this.subscriptions = this.onCanContinue.subscribe(next => {
      this.canContinue = true;
    }, error =>  {
      // TODO
      console.log('An error occurred');
    });
  }

  showHint(): void {
    this.hintCollapsed = !this.hintCollapsed;
  }

  next(): void {
    this.showNext.emit();
  }

  onSelectedPhrase(selectedPhrase: string): void {
    const output = selectedPhrase === this.currentPhrase.translatedPhrase;
    this.guessResult.emit(output);
  }

}
