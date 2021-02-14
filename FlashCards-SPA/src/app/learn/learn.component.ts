import { Component, OnInit } from '@angular/core';
import {AlertifyService} from '../core/_services/alertify.service';
import {FlashcardLearnForSelection} from '../core/_models/flashcardLearnForSelection';
import {FlashcardLearnForPresentation} from '../core/_models/flashcardLearnForPresentation';
import {FlashcardLearnForBlocks} from '../core/_models/flashcardLearnForBlocks';
import {FlashcardLearnForInput} from '../core/_models/flashcardLearnForInput';
import {cssScannerError} from 'codelyzer/angular/styles/cssLexer';
import {Subject, throwError} from 'rxjs';
import {FlashcardForLearn} from '../core/_models/_dtos/fromServer/flashcardForLearn';
import {LearningSessionServiceService} from '../core/_services/learningSession.service';
import {ActivatedRoute, Router} from '@angular/router';
import {FlashcardShort} from '../core/_models/_dtos/fromServer/flashcardShort';
import {LearnTypeEnum} from '../core/_models/enums/learnTypeEnum';
import {LearnSession} from '../core/_others/learnSession';
import {LearnService} from '../core/_services/learn.service';
import {LearnSummary} from '../core/_models/learnSummary';

@Component({
  selector: 'app-learn',
  templateUrl: './learn.component.html',
  styleUrls: ['./learn.component.css']
})
export class LearnComponent implements OnInit {
  // Child components visibility
  phraseBlockComponentActive: boolean;
  phraseInputComponentActive: boolean;
  phrasePresentationComponentActive: boolean;
  phraseSelectionComponentActive: boolean;
  learnStartComponentActive: boolean;
  learnSummaryComponentActive: boolean;

  // NEW
  drawnFlashcards: Array<FlashcardForLearn>;
  repetitionMode: boolean;
  learnMode: boolean;
  learnType: LearnTypeEnum;
  private flashcardsToLearn: Array<any>;
  private learnSession: LearnSession;
  private endOfLearning: boolean;
  lessonName: string;
  learnSummary: LearnSummary;

  // New Block
  canContinue: Subject<any> = new Subject<any>();
  currentFlashcardForSelection: FlashcardLearnForSelection;
  currentFlashcardForPresentation: FlashcardLearnForPresentation;
  currentFlashcardForBlocks: FlashcardLearnForBlocks;
  currentFlashcardForInput: FlashcardLearnForInput;

  constructor(private alertifyService: AlertifyService,
              private learningService: LearningSessionServiceService,
              private route: ActivatedRoute,
              private learnService: LearnService,
              private router: Router) { }

  ngOnInit(): void {
    this.loadDesignData();
    this.route.data.subscribe(data => {
      this.drawnFlashcards = data.learnConfiguration.drawnFlashcards;
      this.learnType = data.learnConfiguration.learnType;
      this.flashcardsToLearn = data.learnConfiguration.flashcards;
      this.lessonName = data.learnConfiguration.lessonName;
    });
    this.setLearnMode(this.learnType);
    this.learnSession = new LearnSession(this.drawnFlashcards, this.flashcardsToLearn);
    this.toggleActiveComponent(4); // Learn-start component
  }

  // NEW FUNCTIONS BLOCK

  startLearning(): void {
    this.endOfLearning = false;
    this.learnSession.startLearning();
    this.learnStartComponentActive = false;
    this.changeActiveComponent(this.learnSession.getCurrentFlashcard());
  }

  onPhraseGuessedResult(result: boolean) {
    this.learnSession.nextFlashcard(result);
    if (this.learnSession.canContinue) {
      this.canContinue.next();
    } else {
      this.endOfLearning = true;
      this.canContinue.next();
    }
  }

  next(): void {
    if (this.endOfLearning) {
      this.learnSummary = this.learnSession.getLearnSummary();
      this.alertifyService.showSuccessAlert('Koniec lekcji');
      this.toggleActiveComponent(5); // Activates summary component
    } else {
      this.changeActiveComponent(this.learnSession.getCurrentFlashcard());
    }
  }

  pauseLearningSession(): void {
    this.alertifyService.showMessageAlert('Pause function');
  }

  quitLearningSession(): void {
    this.alertifyService.showConfirmAlert('Czy na pewno chcesz opuścić lekcję? Postęp nie zostanie zapisany', () => {
      this.router.navigate(['dashboard']);
    });
  }

  onEndLearning(): void {
    this.learnService.sendLearningResult(this.learnSummary.flashcardsAfterLearn).subscribe(next => {
      this.router.navigate(['dashboard']);
    }, error => {
      this.alertifyService.showErrorAlert('Problem with saving learning result.');
    });
  }

  private setLearnMode(learnType: LearnTypeEnum): void {
    switch (learnType){
      case LearnTypeEnum.Learn:
        this.repetitionMode = false;
        this.learnMode = true;
        break;
      case LearnTypeEnum.Repetition:
        this.learnMode = false;
        this.repetitionMode = true;
        break;
    }
  }

  private changeActiveComponent(flashcard: any): void {
    if (flashcard.flashcardType === 'presentation') {
      this.phraseBlockComponentActive = false;
      this.phraseInputComponentActive = false;
      this.phraseSelectionComponentActive = false;
      this.setCurrentFlashcard(flashcard);
      this.phrasePresentationComponentActive = true;
    }
    if (flashcard.flashcardType === 'blocks') {
      this.phraseInputComponentActive = false;
      this.phraseSelectionComponentActive = false;
      this.phrasePresentationComponentActive = false;
      this.setCurrentFlashcard(flashcard);
      this.phraseBlockComponentActive = true;
    }
    if (flashcard.flashcardType === 'selection') {
      this.phraseInputComponentActive = false;
      this.phrasePresentationComponentActive = false;
      this.phraseBlockComponentActive = false;
      this.setCurrentFlashcard(flashcard);
      this.phraseSelectionComponentActive = true;
    }
    if (flashcard.flashcardType === 'input') {
      this.phrasePresentationComponentActive = false;
      this.phraseBlockComponentActive = false;
      this.phraseSelectionComponentActive = false;
      this.setCurrentFlashcard(flashcard);
      this.phraseInputComponentActive = true;
    }
    // else {
    //   console.log('Argument Error: Wrong type of flashcard - ' + typeof(flashcard));
    //   throw new Error('Incorrect type of flashcard: ' + typeof(flashcard));
    // }
  }

  private setCurrentFlashcard(flashcard: any): void {
    if (flashcard.flashcardType === 'presentation') {
      this.currentFlashcardForBlocks = null;
      this.currentFlashcardForInput = null;
      this.currentFlashcardForSelection = null;
      this.currentFlashcardForPresentation = flashcard;
    }
    if (flashcard.flashcardType === 'input') {
      this.currentFlashcardForBlocks = null;
      this.currentFlashcardForSelection = null;
      this.currentFlashcardForPresentation = null;
      this.currentFlashcardForInput = flashcard;
    }
    if (flashcard.flashcardType === 'selection') {
      this.currentFlashcardForBlocks = null;
      this.currentFlashcardForPresentation = null;
      this.currentFlashcardForInput = null;
      this.currentFlashcardForSelection = flashcard;
    }
    if (flashcard.flashcardType === 'blocks') {
      this.currentFlashcardForPresentation = null;
      this.currentFlashcardForInput = null;
      this.currentFlashcardForSelection = null;
      this.currentFlashcardForBlocks = flashcard;
    }
    // else {
    //   console.log('Argument Error: Wrong type of flashcard - ' + typeof(flashcard));
    //   throwError('Wrong type of flashcard');
    // }
  }

  // END BLOCK NEW FUNCTIONS

  testLearn(): void {
    this.learningService.spliceArray();
  }

  testLearnSession(): void {
    // this.learnSession.startLearning();
    // const sth = this.learnSession.getCurrentFlashcard();
    // console.log(sth);
    // const sth1 = this.learnSession.nextFlashcard(false);
    // console.log(sth1);
    // const sth2 = this.learnSession.nextFlashcard(true);
    // console.log(sth2);
    // const sth3 = this.learnSession.nextFlashcard(true);
    // if (sth3 === null) {
    //   this.alertifyService.showSuccessAlert('Zakończono');
    // }
  }

  toggleActiveComponent(option: number): void {
    switch (option) {
      case 0: // Activates flashcard presentation component
        this.phraseBlockComponentActive = false;
        this.phraseInputComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.learnStartComponentActive = false;
        this.learnSummaryComponentActive = false;
        this.phrasePresentationComponentActive = true;
        break;
      case 1: // Activates flashcard block component (used after bad answer)
        this.phraseInputComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phrasePresentationComponentActive = false;
        this.learnStartComponentActive = false;
        this.learnSummaryComponentActive = false;
        this.phraseBlockComponentActive = true;
        break;
      case 2: // Activates flashcard multi selection
        this.phraseInputComponentActive = false;
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.learnStartComponentActive = false;
        this.learnSummaryComponentActive = false;
        this.phraseSelectionComponentActive = true;
        break;
      case 3: // Activates flashcard input component
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.learnStartComponentActive = false;
        this.learnSummaryComponentActive = false;
        this.phraseInputComponentActive = true;
        break;
      case 4: // Activates learn start component
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phraseInputComponentActive = false;
        this.learnSummaryComponentActive = false;
        this.learnStartComponentActive = true;
        break;
      case 5: // Activates learn summary component
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phraseInputComponentActive = false;
        this.learnStartComponentActive = false;
        this.learnSummaryComponentActive = true;
        break;
    }
  }



  private loadDesignData(): void {
    const flashcardsForSelection = new Array<string>();
    flashcardsForSelection.push('nowy');
    flashcardsForSelection.push('stary');
    flashcardsForSelection.push('Uzależniający');
    flashcardsForSelection.push('inny');
    this.currentFlashcardForSelection = new FlashcardLearnForSelection();
    this.currentFlashcardForSelection.initialize(0,
      0,
      'addictive',
      'adiktiw',
      'I am so addictive',
      'This is comment',
      'Uzależniający',
      'Jestem bardzo uzależniający',
      'To jest komentarz',
      'B2',
      'Test category',
      4,
      false,
        new Date());
    this.currentFlashcardForSelection.flashcardsForSelection = flashcardsForSelection;
    const flashcardForLearnTest = new FlashcardForLearn();
    flashcardForLearnTest.initialize(0,
      0,
      'addictive',
      'adiktiw',
      'I am so addictive',
      'This is comment',
      'Uzależniający',
      'Jestem bardzo uzależniający',
      'To jest komentarz',
      'B2',
      'Test category',
      4,
      false,
      new Date());
    this.currentFlashcardForPresentation = new FlashcardLearnForPresentation();
    this.currentFlashcardForPresentation.initializeWithFlashcard(flashcardForLearnTest);
    this.currentFlashcardForPresentation.withInput = false;
    this.currentFlashcardForBlocks = new FlashcardLearnForBlocks();
    this.currentFlashcardForBlocks.initialize(0,
      0,
      'This is for testing',
      'adiktiw',
      'I am so addictive',
      'This is comment',
      'This is for testing',
      'Jestem bardzo uzależniający',
      'To jest komentarz',
      'B2',
      'Test category',
      4,
      false,
      new Date());
    const dividedFlashcardPhraseCorrect = new Array<string>();
    dividedFlashcardPhraseCorrect.push('This');
    dividedFlashcardPhraseCorrect.push('is');
    dividedFlashcardPhraseCorrect.push('for');
    dividedFlashcardPhraseCorrect.push('testing');
    const dividedFlashcardPhraseShuffled = new Array<string>();
    dividedFlashcardPhraseShuffled.push('is');
    dividedFlashcardPhraseShuffled.push('for');
    dividedFlashcardPhraseShuffled.push('testing');
    dividedFlashcardPhraseShuffled.push('This');
    this.currentFlashcardForBlocks.dividedFlashcardPhraseCorrect = dividedFlashcardPhraseCorrect;
    this.currentFlashcardForBlocks.dividedFlashcardPhraseShuffled = dividedFlashcardPhraseShuffled;
    this.currentFlashcardForInput = new FlashcardLearnForInput();
    this.currentFlashcardForInput.initializeWithFlashcard(flashcardForLearnTest);
  }

}
