import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Models/User';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from 'selenium-webdriver/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl: string = 'api/user/';
  constructor(private http: HttpClient) { }

  Login(user: User): Observable<User>{
    return this.http.post<User>(this.apiUrl + 'login', user);
  }

  Register(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl + 'register', user);
  }
}
