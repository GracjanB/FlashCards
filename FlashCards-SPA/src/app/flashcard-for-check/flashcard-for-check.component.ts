import {Component, Input, OnInit} from '@angular/core';
import {Flashcard} from '../core/_models/_dtos/fromServer/flashcard';

@Component({
  selector: 'app-flashcard-for-check',
  templateUrl: './flashcard-for-check.component.html',
  styleUrls: ['./flashcard-for-check.component.css']
})
export class FlashcardForCheckComponent implements OnInit {
  @Input() flashcardForCheck: Flashcard;
  flashcardBodyCollapsed = true;

  constructor() { }

  ngOnInit(): void {
  }

}
