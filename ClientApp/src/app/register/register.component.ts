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
        this.authorizedEvent.emit(this.user);
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
