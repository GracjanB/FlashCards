import {LearnTypeEnum} from './enums/learnTypeEnum';
import {FlashcardShort} from './_dtos/fromServer/flashcardShort';
import {FlashcardForLearn} from './_dtos/fromServer/flashcardForLearn';

export class LearnConfiguration {
  learnType: LearnTypeEnum;
  drawnFlashcards: FlashcardForLearn[];
  lessonName: string;
  flashcards: any[];
}
