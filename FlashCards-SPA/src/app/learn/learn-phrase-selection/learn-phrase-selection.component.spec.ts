import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnPhraseSelectionComponent } from './learn-phrase-selection.component';

describe('LearnPhraseSelectionComponent', () => {
  let component: LearnPhraseSelectionComponent;
  let fixture: ComponentFixture<LearnPhraseSelectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LearnPhraseSelectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LearnPhraseSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
