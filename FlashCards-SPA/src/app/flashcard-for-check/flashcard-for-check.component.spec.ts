import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FlashcardForCheckComponent } from './flashcard-for-check.component';

describe('FlashcardForCheckComponent', () => {
  let component: FlashcardForCheckComponent;
  let fixture: ComponentFixture<FlashcardForCheckComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FlashcardForCheckComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FlashcardForCheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
