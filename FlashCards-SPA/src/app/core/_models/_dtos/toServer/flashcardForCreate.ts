export class FlashcardForCreate {
  phrase: string;
  phrasePronunciation: string;
  phraseSampleSentence: string;
  phraseComment: string;
  translatedPhrase: string;
  translatedPhraseSampleSentence: string;
  translatedPhraseComment: string;
  languageLevel: string;
  category: string;

  constructor(phrase: string,
              phrasePronunciation: string,
              phraseSampleSentence: string,
              phraseComment: string,
              translatedPhrase: string,
              translatedPhraseSampleSentence: string,
              translatedPhraseComment: string,
              languageLevel: string,
              category: string) {
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
