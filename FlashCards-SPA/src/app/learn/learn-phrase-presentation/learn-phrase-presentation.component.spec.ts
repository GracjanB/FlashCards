import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnPhrasePresentationComponent } from './learn-phrase-presentation.component';

describe('LearnPhrasePresentationComponent', () => {
  let component: LearnPhrasePresentationComponent;
  let fixture: ComponentFixture<LearnPhrasePresentationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LearnPhrasePresentationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LearnPhrasePresentationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
