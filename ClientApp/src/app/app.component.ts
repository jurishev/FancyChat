import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {

  isLoggedIn: boolean;
  isRegistering: boolean;

  showRegister() {
    this.isRegistering = true;
  }

  showLogin() {
    this.isRegistering = false;
  }

  showChat() {
    this.isRegistering = false;
    this.isLoggedIn = true;
  }
}
