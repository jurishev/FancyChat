import { Component } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  private connection: HubConnection;
  isConnected: boolean;

  user: string;
  message: string;
  inbox: Envelope[] = [];

  connectToSignalR() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('/chat').build();
    
    this.connection.on("Receive", (usr: string, msg: string) => {
      this.inbox.push(new Envelope(usr, msg));
    });

    this.connection.start();
    this.isConnected = true;
  }

  sengMessage() {
    this.connection.invoke("Broadcast", this.user, this.message);
  }
}

class Envelope {

  user: string;
  message: string;

  constructor(usr: string, msg: string) {
    this.user = usr;
    this.message = msg;
  }
}
