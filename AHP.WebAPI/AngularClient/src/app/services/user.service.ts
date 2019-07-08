import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { of } from 'rxjs';

import { User } from '../classes/user';
import { ChoiceRequest } from '../classes/choice-request';
import { Choice } from '../classes/choice';
import { AlternativeRequest } from '../classes/alternative-request';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userUrl = '/api/user';
  private choiceUrl = '/api/choice';
  private criterionUrl = '/api/criterion';
  private alternativeUrl = '/api/alternative';
  private criteriaComparisonUrl = '/api/criteriaComparison';
  alternativeComparisonUrl = '/api/alternativeComparison';

  public user: BehaviorSubject<User> = new BehaviorSubject<User>(null);
  public choiceRequest: BehaviorSubject<ChoiceRequest> = new BehaviorSubject<ChoiceRequest>(null);

  constructor(private http: HttpClient, private router: Router) {
  }

  register(user: User): Observable<User> {
    return this.http.post<User>(this.userUrl + '/register', user)
      .pipe(
        tap(() => {
          this.router.navigate(['login']);
        }
        )
      );
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

  isLoggedIn() {
    if (localStorage['loggedIn']=='true') {
      return true;
    }
    else {
      return false;
    }
  }

  logout() {
    localStorage.setItem('loggedIn', 'false');
    localStorage.removeItem('userName');
  }

  createChoice(choice: Choice): Observable<Choice> {
    return this.http.post<Choice>(this.choiceUrl + '/postChoice', choice);
  }

  getChoices(choiceRequest: ChoiceRequest): Observable<ChoiceRequest> {
    return this.http.get<ChoiceRequest>(this.choiceUrl + '/get/' + choiceRequest['userId'] + "/1");
  }

  getCriteria(criteriaRequest: ChoiceRequest): Observable<ChoiceRequest> {
    return this.http.get<ChoiceRequest>(this.criterionUrl + '/get/' + criteriaRequest['userId'] + "/1");
  }

  getAlternatives(alternativeRequest: ChoiceRequest): Observable<ChoiceRequest> {
    return this.http.get<ChoiceRequest>(this.alternativeUrl + '/get/' + alternativeRequest['userId'] + "/1");
  }
  
  getCriteriaComparison(criteriaComparisonRequest: ChoiceRequest): Observable<ChoiceRequest> {
    return this.http.get<ChoiceRequest>(this.criteriaComparisonUrl + '/get/' + criteriaComparisonRequest['userId'] + "/1");
  }

  getAlternativeComparison(alternativeComparisonRequest: AlternativeRequest): Observable<AlternativeRequest> {
    return this.http.get<AlternativeRequest>(this.alternativeComparisonUrl + '/get/' + alternativeComparisonRequest['criteriaID']+ "/1");
  }
}
