export class UserForRegister {
  email: string;
  password: string;
  displayName: string;
  firstName: string;
  lastName: string;
  city: string;
  country: string;

  public constructor(init?: Partial<UserForRegister>) {
    Object.assign(this, init);
  }
}
