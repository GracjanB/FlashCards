import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {LearnSummary} from '../../core/_models/learnSummary';
import {AlertifyService} from '../../core/_services/alertify.service';

@Component({
  selector: 'app-learn-summary',
  templateUrl: './learn-summary.component.html',
  styleUrls: ['./learn-summary.component.css']
})
export class LearnSummaryComponent implements OnInit {
  @Input() learnSummary: LearnSummary;
  @Output() endLearning: EventEmitter<any> = new EventEmitter<any>();

  constructor(private alertifyService: AlertifyService) { }

  ngOnInit(): void {
  }

  confirmEndOfLearning(): void {
    this.endLearning.emit();
  }
}
