import {UserRoleEnum} from '../../enums/userRoleEnum';

export class User {
  id: number;
  email: string;
  displayName: string;
  photoUrl: string;
  firstName: string;
  lastName: string;
  city: string;
  country: string;
  role: UserRoleEnum;
  dateCreated: Date;
}
