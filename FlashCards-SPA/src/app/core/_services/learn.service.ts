import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {FlashcardLearnForInput} from '../_models/flashcardLearnForInput';

@Injectable({
  providedIn: 'root'
})
export class LearnService {
  private readonly baseUrl = environment.apiUrl + 'learn/';

  getFlashcardsForLearn(subCourseId: number, subLessonId: number = 0): any {
    return [];
  }

  getFlashcardsForRepetition(subCourseId: number, subLessonId: number = 0): any {
    return [];
  }

  loadTempData(): [] {
    const flashcards = [];
    flashcards.push(new FlashcardLearnForInput(

    ));


    return flashcards;
  }
}
