import { Component } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import { Envelope } from '../envelope';
import { EnvelopeService } from '../envelope.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  providers: [EnvelopeService]
})
export class ChatComponent {

  private connection: HubConnection;
  isConnected: boolean;

  inbox: Envelope[] = [];

  constructor(private envelopeService: EnvelopeService) { }

  connect() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('/chat').build();
    
    this.connection.on("Receive", (usr: string, msg: string) => {
      this.inbox.push(new Envelope(usr, msg));
    });

    this.connection.start();
    this.isConnected = true;
  }

  send(env: Envelope) {
    this.envelopeService.broadcast(env).subscribe();
  }
}
