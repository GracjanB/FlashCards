import {Flashcard} from './flashcard';

export class LessonForCheck {
  id: number;
  name: string;
  flashcards: Array<Flashcard>;

  constructor(id: number,
              name: string,
              flashcards: Array<Flashcard>) {
    this.id = id;
    this.name = name;
    this.flashcards = flashcards;
  }
}
