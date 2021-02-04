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

@Component({
  selector: 'app-learn',
  templateUrl: './learn.component.html',
  styleUrls: ['./learn.component.css']
})
export class LearnComponent implements OnInit {
  phraseBlockComponentActive: boolean;
  phraseInputComponentActive: boolean;
  phrasePresentationComponentActive: boolean;
  phraseSelectionComponentActive: boolean;
  learnStartComponentActive: boolean;
  testLongPhraseForBlocks = 'This is a test phrase block for component';
  actualPhrase = 'to look for';

  // New Block
  canContinue: Subject<any> = new Subject<any>();
  currentFlashcardForSelection: FlashcardLearnForSelection;
  currentFlashcardForPresentation: FlashcardLearnForPresentation;
  currentFlashcardForBlocks: FlashcardLearnForBlocks;
  currentFlashcardForInput: FlashcardLearnForInput;

  constructor(private alertifyService: AlertifyService,
              private learningService: LearningSessionServiceService) { }

  ngOnInit(): void {
    this.loadDesignData();
    this.toggleActiveComponent(4);
  }

  onSelectedPhrase(selectedPhrase: string): void {
    this.alertifyService.showSuccessAlert('Received a selected word' + selectedPhrase);
  }

  next(): void {
    this.alertifyService.showSuccessAlert('Next flashcard');
  }

  onPhraseGuessedResult(result: boolean) {
    this.canContinue.next();
  }

  pauseLearningSession(): void {
    this.alertifyService.showMessageAlert('Pause function');
  }

  quitLearningSession(): void {
    this.alertifyService.showConfirmAlert('Czy na pewno chcesz opuścić lekcję? Postęp nie zostanie zapisany', () => {
      this.alertifyService.showMessageAlert('Opuszczenie lekcji');
    });
  }

  testLearn(): void {
    this.learningService.spliceArray();
  }


  toggleActiveComponent(option: number): void {
    switch (option) {
      case 0: // Activates flashcard presentation component
        this.phraseBlockComponentActive = false;
        this.phraseInputComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.learnStartComponentActive = false;
        this.phrasePresentationComponentActive = true;
        break;
      case 1: // Activates flashcard block component (used after bad answer)
        this.phraseInputComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phrasePresentationComponentActive = false;
        this.learnStartComponentActive = false;
        this.phraseBlockComponentActive = true;
        break;
      case 2: // Activates flashcard multi selection
        this.phraseInputComponentActive = false;
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.learnStartComponentActive = false;
        this.phraseSelectionComponentActive = true;
        break;
      case 3: // Activates flashcard input component
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.learnStartComponentActive = false;
        this.phraseInputComponentActive = true;
        break;
      case 4: // Activates learn start component
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phraseInputComponentActive = false;
        this.learnStartComponentActive = true;
        break;
    }
  }

  private setCurrentFlashcard(flashcard: any): void {
    switch (flashcard) {
      case flashcard instanceof FlashcardLearnForPresentation:
        this.currentFlashcardForBlocks = null;
        this.currentFlashcardForInput = null;
        this.currentFlashcardForSelection = null;
        this.currentFlashcardForPresentation = flashcard;
        break;
      case flashcard instanceof FlashcardLearnForInput:
        this.currentFlashcardForBlocks = null;
        this.currentFlashcardForSelection = null;
        this.currentFlashcardForPresentation = null;
        this.currentFlashcardForInput = flashcard;
        break;
      case flashcard instanceof FlashcardLearnForSelection:
        this.currentFlashcardForBlocks = null;
        this.currentFlashcardForPresentation = null;
        this.currentFlashcardForInput = null;
        this.currentFlashcardForSelection = flashcard;
        break;
      case flashcard instanceof FlashcardLearnForBlocks:
        this.currentFlashcardForPresentation = null;
        this.currentFlashcardForInput = null;
        this.currentFlashcardForSelection = null;
        this.currentFlashcardForBlocks = flashcard;
        break;
      default:
        console.log('Argument Error: Wrong type of flashcard - ' + typeof(flashcard));
        throwError('Wrong type of flashcard');
        break;
    }
  }

  private changeActiveComponent(flashcard: any): void {
    switch (flashcard) {
      case flashcard instanceof FlashcardLearnForPresentation:
        this.phraseBlockComponentActive = false;
        this.phraseInputComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phrasePresentationComponentActive = true;
        break;
      case flashcard instanceof FlashcardLearnForBlocks:
        this.phraseInputComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = true;
        break;
      case flashcard instanceof FlashcardLearnForSelection:
        this.phraseInputComponentActive = false;
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.phraseSelectionComponentActive = true;
        break;
      case flashcard instanceof FlashcardLearnForInput:
        this.phrasePresentationComponentActive = false;
        this.phraseBlockComponentActive = false;
        this.phraseSelectionComponentActive = false;
        this.phraseInputComponentActive = true;
        break;
      default:
        console.log('Argument Error: Wrong type of flashcard - ' + typeof(flashcard));
        throwError('Wrong type of flashcard');
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
