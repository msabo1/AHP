import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { of } from 'rxjs';

import { User } from '../classes/user';
import { ChoiceRequest } from '../classes/choice-request';
import { Choice } from '../classes/choice';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userUrl = '/api/user';
  private choiceUrl = '/api/choice';
  public user: BehaviorSubject<User> = new BehaviorSubject<User>(null);
  public choiceRequest: BehaviorSubject<ChoiceRequest> = new BehaviorSubject<ChoiceRequest>(null);

  constructor(private http: HttpClient, private router: Router) {
  }

  register(user: User): Observable<User> {
    return this.http.post<User>(this.userUrl+'/register', user);
  }

  login(user: User): Observable<User> {
    return this.http.post<User>(this.userUrl + '/login', user)
      .pipe(
        tap(() => {
          this.router.navigate(['choices']);
        }
       )
     );
  }
  getChoices(choiceRequest: ChoiceRequest): Observable<ChoiceRequest> {
    return this.http.post<ChoiceRequest>(this.choiceUrl + '/get/', choiceRequest);
  }
}
