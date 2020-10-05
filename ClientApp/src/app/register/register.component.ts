import { Component, Output, EventEmitter } from '@angular/core';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  providers: [UserService]
})
export class RegisterComponent {

  @Output() loginClickEvent = new EventEmitter();
  @Output() authorizedEvent = new EventEmitter();

  user = new User();
  isLoginTaken: boolean;

  constructor(private userService: UserService) { }

  gotoLogin() {
    this.resetFlags();
    this.loginClickEvent.emit();
  }

  submit() {
    this.userService.create(this.user)
      .subscribe(_ => {
        this.userService.login(this.user)
          .subscribe(response => {
            localStorage.setItem('fancy-chat-jwt', (<any>response).token);
            this.authorizedEvent.emit();
          });
      }, error => {
        if (error.status === 400) {
          this.isLoginTaken = true;
        }
      });
  }

  resetFlags() {
    this.isLoginTaken = false;
  }
}
