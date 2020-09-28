import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';

@Injectable()
export class UserService {

  constructor(private http: HttpClient) { }

  get(login: string) {
    return this.http.get<User>(`http://localhost:5000/api/users/${login}`);
  }

  create(user: User) {
    return this.http.post('http://localhost:5000/api/users', user);
  }
}
