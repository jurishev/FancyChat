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

  login: string;
  password: string;

  isEmptyForm: boolean;

  gotoRegister() {
    this.registerClickEvent.emit();
  }

  submit() {
    if (!this.login || !this.password) {
      this.isEmptyForm = true;
      return;
    }   
  }
}
