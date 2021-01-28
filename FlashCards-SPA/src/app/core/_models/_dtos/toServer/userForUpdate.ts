export class UserForUpdate {
  firstName: string;
  lastName: string;
  displayName: string;
  city: string;
  country: string;
  numberOfWordsInLearningSession: number;
  numberOfWordsInReviewSession: number;

  constructor(firstName: string,
              lastName: string,
              displayName: string,
              city: string,
              country: string,
              numberOfWordsInLearningSession: number,
              numberOfWordsInReviewSession: number) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.displayName = displayName;
    this.city = city;
    this.country = country;
    this.numberOfWordsInLearningSession = numberOfWordsInLearningSession;
    this.numberOfWordsInReviewSession = numberOfWordsInReviewSession;
  }

}
