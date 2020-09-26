import { Component, Output, EventEmitter } from '@angular/core';
import { Envelope } from '../envelope';

@Component({
  selector: 'app-user-input',
  templateUrl: './user-input.component.html'
})
export class UserInputComponent {

  @Output() newInputEvent = new EventEmitter<Envelope>();

  user: string;
  message: string;

  send() {
    this.newInputEvent.emit(new Envelope(this.user, this.message));
  }
}
