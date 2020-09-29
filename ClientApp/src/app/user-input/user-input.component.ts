import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Envelope } from '../envelope';
import { User } from '../user';

@Component({
  selector: 'app-user-input',
  templateUrl: './user-input.component.html'
})
export class UserInputComponent {

  @Output() newInputEvent = new EventEmitter<Envelope>();

  @Input() user: User;
  message: string;

  send() {
    this.newInputEvent.emit(new Envelope(this.user.login, this.message));
  }
}
