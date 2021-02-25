import {FlashcardShort} from './flashcardShort';

export class Lesson {
  id: number;
  subLessonId: number;
  subCourseId: number;
  name: string;
  description: string;
  category: string;
  dateCreated: Date;
  dateModified: Date;
  isSubscribed: boolean;
  overallProgress: number;
  flashcards: FlashcardShort[];

  constructor(id: number, subLessonId: number, subCourseId: number, name: string, description: string,
              category: string, dateCreated: Date, dateModified: Date,
              isSubscribed: boolean, overallProgress: number,
              flashcards: FlashcardShort[]) {
    this.id = id;
    this.subLessonId = subLessonId;
    this.subCourseId = subCourseId;
    this.name = name;
    this.description = description;
    this.category = category;
    this.dateCreated = dateCreated;
    this.dateModified = dateModified;
    this.isSubscribed = isSubscribed;
    this.overallProgress = overallProgress;
    this.flashcards = flashcards;
  }
}
