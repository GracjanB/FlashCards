import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnPhraseInputComponent } from './learn-phrase-input.component';

describe('LearnPhraseInputComponent', () => {
  let component: LearnPhraseInputComponent;
  let fixture: ComponentFixture<LearnPhraseInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LearnPhraseInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LearnPhraseInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
