import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseCheckPanelComponent } from './course-check-panel.component';

describe('CourseCheckPanelComponent', () => {
  let component: CourseCheckPanelComponent;
  let fixture: ComponentFixture<CourseCheckPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CourseCheckPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseCheckPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
