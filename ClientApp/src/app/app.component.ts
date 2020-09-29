import { Component } from '@angular/core';
import { User } from './user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {

  isLoggedIn: boolean;
  isRegistering: boolean;

  user = new User();

  showRegister() {
    this.isRegistering = true;
  }

  showLogin() {
    this.isRegistering = false;
  }

  showChat(user: User) {
    this.user = user;
    this.isRegistering = false;
    this.isLoggedIn = true;
  }
}
