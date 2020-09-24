import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Envelope } from './envelope';

@Injectable()
export class EnvelopeService {

  constructor(private http: HttpClient) { }

  broadcast(env: Envelope) {
    return this.http.post('http://localhost:5000/api/signalr', env);
  }
}
