export class UserForLogin {
  email: string;
  password: string;

  public constructor(init?: Partial<UserForLogin>) {
    Object.assign(this, init);
  }
}
