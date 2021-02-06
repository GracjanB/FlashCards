import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FlashcardForLearn} from '../../core/_models/_dtos/fromServer/flashcardForLearn';
import {LearnTypeEnum} from '../../core/_models/enums/learnTypeEnum';

@Component({
  selector: 'app-learn-start',
  templateUrl: './learn-start.component.html',
  styleUrls: ['./learn-start.component.css']
})
export class LearnStartComponent implements OnInit {
  @Input() flashcardsToLearn: FlashcardForLearn[];
  @Input() learnType: LearnTypeEnum;
  @Output() startLearning: EventEmitter<any> = new EventEmitter<any>();
  learnMode: boolean;
  repetitionMode: boolean;

  constructor() { }

  ngOnInit(): void {
    this.setLearnMode(this.learnType);
  }

  start(): void {
    this.startLearning.emit();
  }

  private setLearnMode(learnType: LearnTypeEnum): void {
    switch (learnType){
      case LearnTypeEnum.Learn:
        this.learnMode = true;
        this.repetitionMode = false;
        break;
      case LearnTypeEnum.Repetition:
        this.learnMode = false;
        this.repetitionMode = true;
        break;
    }
  }
}
