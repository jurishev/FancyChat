import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {

  @Output() loginClickEvent = new EventEmitter();

  login: string;
  password: string;
  country: string;
  city: string;

  isEmptyForm: boolean;

  gotoLogin() {
    this.loginClickEvent.emit();
  }

  submit() {
    if (!this.login || !this.password) {
      this.isEmptyForm = true;
      return;
    }
  }
}
