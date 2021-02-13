import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonForCheckCardComponent } from './lesson-for-check-card.component';

describe('LessonForCheckCardComponent', () => {
  let component: LessonForCheckCardComponent;
  let fixture: ComponentFixture<LessonForCheckCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LessonForCheckCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonForCheckCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
