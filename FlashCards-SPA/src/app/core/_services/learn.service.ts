import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {LearnConfiguration} from '../_models/learnConfiguration';
import {FlashcardShort} from '../_models/_dtos/fromServer/flashcardShort';
import {LearnTypeEnum} from '../_models/enums/learnTypeEnum';
import {FlashcardLearnForInput} from '../_models/flashcardLearnForInput';
import {FlashcardLearnForSelection} from '../_models/flashcardLearnForSelection';
import {FlashcardForLearn} from '../_models/_dtos/fromServer/flashcardForLearn';
import {Observable} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LearnAdapter} from '../_adapters/learnAdapter';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LearnService {
  private readonly baseUrl = environment.apiUrl + 'learn/';

  constructor(private httpClient: HttpClient,
              private learnAdapter: LearnAdapter) {
  }

  getFlashcardsForLearn(subCourseId: number): Observable<LearnConfiguration> {
    const url = this.baseUrl + 'course/' + subCourseId;
    return this.httpClient.get(url, {
      observe: 'response'
    }).pipe(map(response => {
      return this.learnAdapter.adaptLearnConfiguration(response.body);
    }));
  }

  getFlashcardsForLearnFromExactLesson(subCourseId: number, subLessonId: number): Observable<LearnConfiguration> {
    const url = this.baseUrl + 'course/' + subCourseId + '/lesson/' + subLessonId;
    return this.httpClient.get(url, {
      observe: 'response'
    }).pipe(map(response => {
      return this.learnAdapter.adaptLearnConfiguration(response.body);
    }));
  }

  getFlashcardsForRepetition(subCourseId: number): Observable<LearnConfiguration> {
    const url = this.baseUrl + 'repetition/course/' + subCourseId;
    return this.httpClient.get(url, {
      observe: 'response'
    }).pipe(map(response => {
      return this.learnAdapter.adaptLearnConfiguration(response.body);
    }));
  }

  getFlashcardsForRepetitionFromExactLesson(subCourseId: number, subLessonId: number): Observable<LearnConfiguration> {
    const url = this.baseUrl + 'repetition/course/' + subCourseId + '/lesson/' + subLessonId;
    return this.httpClient.get(url, {
      observe: 'response'
    }).pipe(map(response => {
      return this.learnAdapter.adaptLearnConfiguration(response.body);
    }));
  }

  sendLearningResult(flashcards: Array<FlashcardForLearn>): Observable<any> {
    const url = this.baseUrl + 'learnResult';
    return this.httpClient.post(url, flashcards, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      })
    });
  }

  loadDesignData(): LearnConfiguration {
    const learnConfiguration = new LearnConfiguration();
    const drawnFlashcards = [];
    const flashcardToLearn = [];
    drawnFlashcards.push(new FlashcardShort(
      0,
      0,
      'addictive',
      'uzależniający',
      true,
      4,
      false,
      false));
    drawnFlashcards.push(new FlashcardShort(
      0,
      0,
      'to look for',
      'szukać',
      true,
      3,
      true,
      false));
    drawnFlashcards.push(new FlashcardShort(
      0,
      0,
      'go',
      'iść',
      true,
      6,
      false,
      false));
    drawnFlashcards.push(new FlashcardShort(
      0,
      0,
      'research',
      'badanie',
      true,
      4,
      false,
      false));
    drawnFlashcards.push(new FlashcardShort(
      0,
      0,
      'independent',
      'niezależny',
      true,
      2,
      true,
      false));
    const flashcardForInput1 = new FlashcardLearnForInput();
    flashcardForInput1.flashcardId = 1;
    flashcardForInput1.phrase = 'addictive';
    flashcardForInput1.translatedPhrase = 'uzależniający';
    flashcardForInput1.markedAsHard = false;
    flashcardForInput1.ordinalNumber = 1;
    flashcardToLearn.push(flashcardForInput1);
    const flashcardForSelection = new FlashcardLearnForSelection();
    flashcardForSelection.phrase = 'addictive';
    flashcardForSelection.flashcardId = 1;
    flashcardForSelection.translatedPhrase = 'uzależniający';
    flashcardForSelection.markedAsHard = false;
    flashcardForSelection.flashcardsForSelection = new Array<string>();
    flashcardForSelection.flashcardsForSelection.push('uzależniający');
    flashcardForSelection.flashcardsForSelection.push('robiony');
    flashcardForSelection.flashcardsForSelection.push('inny');
    flashcardForSelection.flashcardsForSelection.push('okoń');
    flashcardForSelection.ordinalNumber = 2;
    flashcardToLearn.push(flashcardForSelection);

    learnConfiguration.drawnFlashcards = this.loadFlashcardsForLearn();
    learnConfiguration.flashcards = flashcardToLearn;
    learnConfiguration.learnType = LearnTypeEnum.Learn;
    return learnConfiguration;
  }

  loadFlashcardsForLearn(): FlashcardForLearn[] {
    const flashcardsForLearn = [];
    const flashcard1 = new FlashcardForLearn();
    flashcard1.flashcardId = 1;
    flashcard1.flashcardSubscriptionId = 1;
    flashcard1.phrase = 'addictive';
    flashcard1.phrasePronunciation = 'adiktiw';
    flashcard1.translatedPhrase = 'uzależniający';
    flashcard1.trainLevel = 3;
    flashcard1.markedAsHard = false;
    const flashcard2 = new FlashcardForLearn();
    flashcard2.flashcardId = 2;
    flashcard2.flashcardSubscriptionId = 2;
    flashcard2.phrase = 'to look for';
    flashcard2.phrasePronunciation = 'tu luk for';
    flashcard2.translatedPhrase = 'szukać';
    flashcard2.trainLevel = 6;
    flashcard2.markedAsHard = false;
    const flashcard3 = new FlashcardForLearn();
    flashcard3.flashcardId = 3;
    flashcard3.flashcardSubscriptionId = 3;
    flashcard3.phrase = 'independent';
    flashcard3.phrasePronunciation = 'independent';
    flashcard3.translatedPhrase = 'niezależny';
    flashcard3.trainLevel = 2;
    flashcard3.markedAsHard = true;
    flashcardsForLearn.push(flashcard1);
    flashcardsForLearn.push(flashcard2);
    flashcardsForLearn.push(flashcard3);
    return flashcardsForLearn;
  }
}
