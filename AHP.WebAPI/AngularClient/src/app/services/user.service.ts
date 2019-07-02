import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { of } from 'rxjs';

import { User } from '../classes/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userUrl = '/api/user';
  public user: BehaviorSubject<User> = new BehaviorSubject<User>(null);

  constructor(private http: HttpClient) { }

  register(user: User): Observable<User> {
    return this.http.post<User>(this.userUrl, user);
  }

  login(user: User): Observable<User> {
    return this.http.get<User>(this.userUrl, user); ///////   POPRAVIT
  }
}
