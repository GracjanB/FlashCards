import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubCourseCardComponent } from './sub-course-card.component';

describe('CourseCardComponent', () => {
  let component: SubCourseCardComponent;
  let fixture: ComponentFixture<SubCourseCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubCourseCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubCourseCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
