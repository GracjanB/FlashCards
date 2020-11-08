import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {FlashcardAdapter} from '../_adapters/flashcardAdapter';
import {Observable} from 'rxjs';
import {Flashcard} from '../_models/_dtos/fromServer/flashcard';
import {environment} from '../../../environments/environment';
import {map} from 'rxjs/operators';
import {FlashcardForCreate} from '../_models/_dtos/toServer/flashcardForCreate';
import {FlashcardForUpdate} from '../_models/_dtos/toServer/flashcardForUpdate';

@Injectable({
  providedIn: 'root'
})
export class FlashcardService {
  baseUrl = environment.apiUrl + 'courses/';

  constructor(private httpClient: HttpClient,
              private flashcardAdapter: FlashcardAdapter) {

  }

  getFlashcard(courseId: number, lessonId: number, id: number): Observable<Flashcard> {
    const url = this.baseUrl + courseId + '/lessons/' + lessonId + '/flashcards/' + id;
    return this.httpClient.get(url, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.flashcardAdapter.adaptFlashcard(response.body);
      })
    );
  }

  createFlashcard(courseId: number, lessonId: number, flashcardForCreate: FlashcardForCreate): Observable<Flashcard> {
    const url = this.baseUrl + courseId + '/lessons/' + lessonId + '/flashcards';
    return this.httpClient.post(url, flashcardForCreate, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.flashcardAdapter.adaptFlashcard(response.body);
      })
    );
  }

  updateFlashcard(courseId: number, lessonId: number, flashcardId: number,
                  flashcardForUpdate: FlashcardForUpdate): Observable<Flashcard> {
    const url = this.baseUrl + courseId + '/lessons/' + lessonId + '/flashcards/' + flashcardId;
    return this.httpClient.put(url, flashcardForUpdate, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token')
      }),
      observe: 'response'
    }).pipe(
      map(response => {
        return this.flashcardAdapter.adaptFlashcard(response.body);
      })
    );
  }
}
