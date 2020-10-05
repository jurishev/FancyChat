import { Component, Output, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import { Envelope } from '../envelope';
import { EnvelopeService } from '../envelope.service';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  providers: [EnvelopeService, UserService]
})
export class ChatComponent {

  @Output() logoutEvent = new EventEmitter();

  private connection: HubConnection;
  isConnected = false;

  user = new User();
  inbox: Envelope[] = [];

  constructor(private envelopeService: EnvelopeService, private userService: UserService) { }

  connect() {
    this.userService.get()
      .subscribe((user: User) => {
        this.user = user;
      }, error => {
        this.user.login = 'undefined';
      });

    this.connection = new signalR.HubConnectionBuilder().withUrl('/chat').build();
    
    this.connection.on("Receive", (usr: string, msg: string) => {
      this.inbox.push(new Envelope(usr, msg));
    });

    this.connection.start();
    this.isConnected = true;
  }

  send(env: Envelope) {
    this.envelopeService.broadcast(env).subscribe();
  }

  logout() {
    this.logoutEvent.emit();
  }
}
