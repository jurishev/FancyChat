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
  
  user = new User();
  notFound: boolean;
  unauthorized: boolean;

  constructor(private userService: UserService) { }

  gotoRegister() {
    this.resetFlags();
    this.registerClickEvent.emit();
  }

  submit() {
    this.userService.login(this.user)
      .subscribe(response => {
        localStorage.setItem('fancy-chat-jwt', (<any>response).token);
        this.authorizedEvent.emit();
      }, error => {
        if (error.status === 404) {
          this.notFound = true;
        }
        else if (error.status === 401) {
          this.unauthorized = true;
        }
      });
  }

  resetFlags() {
    this.notFound = false;
    this.unauthorized = false;
  }
}
