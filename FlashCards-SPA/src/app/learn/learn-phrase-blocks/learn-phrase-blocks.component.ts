import {AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {FlashcardLearnForBlocks} from '../../core/_models/flashcardLearnForBlocks';
import {Observable, Subscription} from 'rxjs';

@Component({
  selector: 'app-learn-phrase-blocks',
  templateUrl: './learn-phrase-blocks.component.html',
  styleUrls: ['./learn-phrase-blocks.component.css']
})
export class LearnPhraseBlocksComponent implements OnInit, AfterViewInit, OnDestroy {
  private subscriptions: Subscription;
  @ViewChild('phraseInput') phraseInput: ElementRef;
  @Input() actualPhrase: string;
  @Input() currentPhrase: FlashcardLearnForBlocks;
  @Input() onCanContinue: Observable<any>;
  @Output() guessResult: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() showNext: EventEmitter<any> = new EventEmitter<any>();
  dividedPhraseList: Array<string>;
  hintCollapsed: boolean;
  phraseGuessed: boolean;
  canContinue = false;

  constructor() {
    this.hintCollapsed = true;
    this.phraseGuessed = false;
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  ngOnInit() {
    console.log(this.currentPhrase);
    this.subscriptions = this.onCanContinue.subscribe(next => {
      this.canContinue = true;
    }, error =>  {
      // TODO
      console.log('An error occurred');
    });
  }

  ngAfterViewInit(): void {
    this.phraseInput.nativeElement.value = '';
  }

  showHint(): void {
    this.hintCollapsed = !this.hintCollapsed;
  }

  next(): void {
    this.showNext.emit();
  }

  addToInput(word: any): void {
    this.phraseInput.nativeElement.value = this.phraseInput.nativeElement.value + word + ' ';
    if (this.phraseInput.nativeElement.value.substring(0, this.phraseInput.nativeElement.value.length - 1) === this.currentPhrase.translatedPhrase) {
      this.phraseGuessed = true;
      this.guessResult.emit(true);
    }
  }

  inputBackspace(): void {
    const dividedPhraseTemp = this.phraseInput.nativeElement.value.split(' ');
    // It's 'length - 2' because the last element is always "", idk why
    const lastItemLength = dividedPhraseTemp[dividedPhraseTemp.length - 2].length;
    const currentTextInInput = this.phraseInput.nativeElement.value;
    this.phraseInput.nativeElement.value = currentTextInInput.substring(0, currentTextInInput.length - (lastItemLength + 1));
  }

  private shuffle(array: Array<any>): Array<any> {
    let currentIndex = array.length;
    let temporaryValue;
    let randomIndex;

    while (0 !== currentIndex) {
      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex -= 1;
      temporaryValue = array[currentIndex];
      array[currentIndex] = array[randomIndex];
      array[randomIndex] = temporaryValue;
    }

    return array;
  }
}
