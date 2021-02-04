import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnSummaryComponent } from './learn-summary.component';

describe('LearnSummaryComponent', () => {
  let component: LearnSummaryComponent;
  let fixture: ComponentFixture<LearnSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LearnSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LearnSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
