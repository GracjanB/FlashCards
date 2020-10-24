import {FlashcardShort} from './flashcardShort';

export class Lesson {
  id: number;
  name: string;
  description: string;
  category: string;
  dateCreated: Date;
  dateModified: Date;
  flashcards: FlashcardShort[];

  constructor(id: number, name: string, description: string,
              category: string, dateCreated: Date, dateModified: Date,
              flashcards: FlashcardShort[]) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.category = category;
    this.dateCreated = dateCreated;
    this.dateModified = dateModified;
    this.flashcards = flashcards;
  }
}
