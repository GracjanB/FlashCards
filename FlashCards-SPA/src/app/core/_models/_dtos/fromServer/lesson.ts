import {FlashcardShort} from './flashcardShort';

export class Lesson {
  id: number;
  name: string;
  description: string;
  category: string;
  dateCreated: Date;
  dateModified: Date;
  isSubscribed: boolean;
  overallProgress: number;
  flashcards: FlashcardShort[];

  constructor(id: number, name: string, description: string,
              category: string, dateCreated: Date, dateModified: Date,
              isSubscribed: boolean, overallProgress: number,
              flashcards: FlashcardShort[]) {
    this.id = id;
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
