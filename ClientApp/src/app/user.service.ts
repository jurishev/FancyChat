import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';

@Injectable()
export class UserService {

  constructor(private http: HttpClient) { }

  create(user: User) {
    return this.http.post('http://localhost:5000/api/users', user);
  }

  login(user: User) {
    return this.http.post('http://localhost:5000/api/auth/login', user);
  }  

  get() {
    return this.http.get('http://localhost:5000/api/auth/user');
  }
}
