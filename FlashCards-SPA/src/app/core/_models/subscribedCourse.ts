export class SubscribedCourse {
  id: number;
  name: string;
  knownFlashcards: number;
  allFlashcards: number;
  progress: number;
  photoUrl: string;

  constructor(id: number, name: string, knownFlashcards: number,
              allFlashcards: number, progress: number, photoUrl: string ) {
    this.id = id;
    this.name = name;
    this.knownFlashcards = knownFlashcards;
    this.allFlashcards = allFlashcards;
    this.progress = progress;
    this.photoUrl = photoUrl;
  }
}
