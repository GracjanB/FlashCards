import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FlashcardLearnForPresentation} from '../../core/_models/flashcardLearnForPresentation';

@Component({
  selector: 'app-learn-phrase-presentation',
  templateUrl: './learn-phrase-presentation.component.html',
  styleUrls: ['./learn-phrase-presentation.component.css']
})
export class LearnPhrasePresentationComponent implements OnInit {
  flashcardMoreInfoCollapsed: boolean;
  translatedFlashcardMoreInfoCollapsed: boolean;
  @Input() currentPhrase: FlashcardLearnForPresentation;
  @Output() showNext: EventEmitter<any> = new EventEmitter<any>();

  constructor() {
    this.flashcardMoreInfoCollapsed = true;
    this.translatedFlashcardMoreInfoCollapsed = true;
  }

  ngOnInit(): void {
  }

  next(): void {
    this.showNext.emit();
  }
}
