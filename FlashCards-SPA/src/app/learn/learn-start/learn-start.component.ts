import { Component, OnInit } from '@angular/core';
import {FlashcardShort} from '../../core/_models/_dtos/fromServer/flashcardShort';

@Component({
  selector: 'app-learn-start',
  templateUrl: './learn-start.component.html',
  styleUrls: ['./learn-start.component.css']
})
export class LearnStartComponent implements OnInit {
  flashcardsToLearn: FlashcardShort[];

  constructor() { }

  ngOnInit(): void {
    this.loadDesignData();
  }

  loadDesignData(): void {
    const flashcards: FlashcardShort[] = [];
    flashcards.push(new FlashcardShort(
      0,
      0,
      'dsafsdaf',
      'sadfasdf',
      true,
      4,
      true,
      false));
    flashcards.push(new FlashcardShort(
      0,
      0,
      'dsafsdaf',
      'sadfasdf',
      true,
      4,
      true,
      false));
    flashcards.push(new FlashcardShort(
      0,
      0,
      'dsafsdaf',
      'sadfasdf',
      true,
      4,
      true,
      false));
    flashcards.push(new FlashcardShort(
      0,
      0,
      'dsafsdaf',
      'sadfasdf',
      true,
      4,
      true,
      false));
    flashcards.push(new FlashcardShort(
      0,
      0,
      'dsafsdaf',
      'sadfasdf',
      true,
      4,
      true,
      false));
    this.flashcardsToLearn = flashcards;
  }
}
