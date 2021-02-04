import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LearnStartComponent } from './learn-start.component';

describe('LearnStartComponent', () => {
  let component: LearnStartComponent;
  let fixture: ComponentFixture<LearnStartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LearnStartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LearnStartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
