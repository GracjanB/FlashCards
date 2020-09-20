import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseGenComponent } from './course-gen.component';

describe('CourseGenComponent', () => {
  let component: CourseGenComponent;
  let fixture: ComponentFixture<CourseGenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CourseGenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseGenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
