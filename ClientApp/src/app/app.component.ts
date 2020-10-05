import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  isLoggedIn = false;
  isRegistering = false;

  ngOnInit() {
    if (this.hasToken()) {
      this.isLoggedIn = true;
    }
  }

  showRegister() {
    this.isRegistering = true;
  }

  showLogin() {
    this.isRegistering = false;
  }

  showChat() {    
    if (this.hasToken()) {
      this.isLoggedIn = true;
      this.isRegistering = false;
    }
  }

  logout() {
    localStorage.removeItem('fancy-chat-jwt');
    this.isLoggedIn = false;
  }

  hasToken(): boolean {
    let token = localStorage.getItem('fancy-chat-jwt');
    return token ? true : false;
  }
}
