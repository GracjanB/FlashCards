import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseForCheckCardComponent } from './course-for-check-card.component';

describe('CourseForCheckCardComponent', () => {
  let component: CourseForCheckCardComponent;
  let fixture: ComponentFixture<CourseForCheckCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CourseForCheckCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseForCheckCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
