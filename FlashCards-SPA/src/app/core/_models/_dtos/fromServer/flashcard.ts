export class Flashcard {
  id: number;
  phrase: string;
  phrasePronunciation: string;
  phraseSampleSentence: string;
  phraseComment: string;
  translatedPhrase: string;
  translatedPhraseSampleSentence: string;
  translatedPhraseComment: string;
  languageLevel: string;
  category: string;

  constructor(id: number,
              phrase: string,
              phrasePronunciation: string,
              phraseSampleSentence: string,
              phraseComment: string,
              translatedPhrase: string,
              translatedPhraseSampleSentence: string,
              translatedPhraseComment: string,
              languageLevel: string,
              category: string) {
    this.id = id;
    this.phrase = phrase;
    this.phrasePronunciation = phrasePronunciation;
    this.phraseSampleSentence = phraseSampleSentence;
    this.phraseComment = phraseComment;
    this.translatedPhrase = translatedPhrase;
    this.translatedPhraseSampleSentence = translatedPhraseSampleSentence;
    this.translatedPhraseComment = translatedPhraseComment;
    this.languageLevel = languageLevel;
    this.category = category;
  }
}
