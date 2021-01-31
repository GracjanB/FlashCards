export class UserDetailed {
  id: number;
  accountId: number;
  email: string;
  displayName: string;
  photoUrl: string;
  firstName: string;
  lastName: string;
  city: string;
  country: string;
  dateCreated: Date;
  numberOfWordsInLearningSession: number;
  numberOfWordsInReviewSession: number;

  constructor(id: number,
              accountId: number,
              email: string,
              displayName: string,
              photoUrl: string,
              firstName: string,
              lastName: string,
              city: string,
              country: string,
              dateCreated: Date,
              numberOfWordsInLearningSession: number,
              numberOfWordsInReviewSession: number) {
    this.id = id;
    this.accountId = accountId;
    this.email = email;
    this.displayName = displayName;
    this.photoUrl = photoUrl;
    this.firstName = firstName;
    this.lastName = lastName;
    this.city = city;
    this.country = country;
    this.dateCreated = dateCreated;
    this.numberOfWordsInLearningSession = numberOfWordsInLearningSession;
    this.numberOfWordsInReviewSession = numberOfWordsInReviewSession;
  }
}
