import { Injectable } from '@angular/core';
import { LessonShort } from '../_models/_dtos/fromServer/lessonShort';
import {FlashcardShort} from '../_models/_dtos/fromServer/flashcardShort';
import {FlashcardAdapter} from './flashcardAdapter';
import {Lesson} from '../_models/_dtos/fromServer/lesson';
import {LessonForCreate} from '../_models/_dtos/toServer/lessonForCreate';
import {SubscribedLessonShort} from '../_models/_dtos/fromServer/subscribedLessonShort';
import {LessonForCheck} from '../_models/_dtos/fromServer/lessonForCheck';
import {Flashcard} from '../_models/_dtos/fromServer/flashcard';

@Injectable({
  providedIn: 'root'
})
export class LessonAdapter {

  constructor(private flashcardAdapter: FlashcardAdapter) { }

  adaptLessonShort(lesson: any, courseId: number): LessonShort {
    return new LessonShort(lesson.id, lesson.name, lesson.category, courseId);
  }

  adaptLesson(lesson: any): Lesson {
    const flashcardsFromArg = lesson.flashcards as [];
    const flashcards: FlashcardShort[] = [];
    for (const flashcard of flashcardsFromArg) {
      flashcards.push(this.flashcardAdapter.adaptFlashcardShort(flashcard));
    }

    return new Lesson(
      lesson.id,
      lesson.name,
      lesson.description,
      lesson.category,
      new Date(lesson.dateCreated),
      new Date(lesson.dateModified),
      lesson.isSubscribed,
      lesson.overallProgress,
      flashcards
    );
  }

  adaptLessonForCreate(lesson: any): LessonForCreate {
    return new LessonForCreate(lesson.name, lesson.description, lesson.category);
  }

  adaptSubscribedLessonShort(lesson: any): SubscribedLessonShort {
    return new SubscribedLessonShort(lesson.id, lesson.name, lesson.progress);
  }

  adaptLessonForCheck(lesson: any): LessonForCheck {
    const flashcardsFromArg = lesson.flashcards;
    const flashcards: Array<Flashcard> = new Array<Flashcard>();
    for (const flashcard of flashcardsFromArg) {
      flashcards.push(this.flashcardAdapter.adaptFlashcard(flashcard));
    }
    return new LessonForCheck(lesson.id, lesson.name, flashcards);
  }
}
