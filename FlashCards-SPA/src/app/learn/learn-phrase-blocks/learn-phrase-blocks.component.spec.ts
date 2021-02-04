import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnPhraseBlocksComponent } from './learn-phrase-blocks.component';

describe('LearnPhraseBlocksComponent', () => {
  let component: LearnPhraseBlocksComponent;
  let fixture: ComponentFixture<LearnPhraseBlocksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LearnPhraseBlocksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LearnPhraseBlocksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
