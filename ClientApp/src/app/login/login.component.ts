import { Component, Output, EventEmitter } from '@angular/core';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [UserService]
})
export class LoginComponent {

  @Output() registerClickEvent = new EventEmitter();
  @Output() authorizedEvent = new EventEmitter();
  
  login: string;
  password: string;

  userNotFound: boolean;
  wrongPassword: boolean;

  constructor(private userService: UserService) { }

  gotoRegister() {
    this.resetFlags();
    this.registerClickEvent.emit();
  }

  submit() {
    this.userService.get(this.login)
      .subscribe((user: User) => {
        if (user.password === this.password) {
          this.authorizedEvent.emit();
        }
        else {
          this.wrongPassword = true;
        }
      }, error => {
        if (error.status === 404) {
          this.userNotFound = true;
        }
      });
  }

  resetFlags() {
    this.userNotFound = false;
    this.wrongPassword = false;
  }
}
