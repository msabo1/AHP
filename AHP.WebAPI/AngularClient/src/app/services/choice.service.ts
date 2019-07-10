import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { ChoiceRequest } from '../classes/choice-request';
import { Choice } from '../classes/choice';

@Injectable({
  providedIn: 'root'
})
export class ChoiceService {

  private choiceUrl = '/api/choice';

  constructor(private http: HttpClient) { }

  createChoice(choice: Choice): Observable<Choice> {
    return this.http.post<Choice>(this.choiceUrl + "/postChoice/", choice);
  }

  getChoices(choiceRequest: ChoiceRequest): Observable<ChoiceRequest> {
    return this.http.get<ChoiceRequest>(this.choiceUrl + '/get/' + choiceRequest['userId'] + "/1");
  }

  deleteChoice(choice: Choice): Observable<Choice> {
    return this.http.post<Choice>(this.choiceUrl + "/deleteChoice/", choice);
  }
}
