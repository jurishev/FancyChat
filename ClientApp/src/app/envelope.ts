export class Envelope {

  user: string;
  message: string;

  constructor(usr: string, msg: string) {
    this.user = usr;
    this.message = msg;
  }
}
