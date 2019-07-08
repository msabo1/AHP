import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AlternativeRequest } from '../classes/alternative-request';
import { ChoiceRequest } from '../classes/choice-request';

@Injectable({
  providedIn: 'root'
})
export class AlternativeService {

  private alternativeUrl = '/api/alternative';
  private alternativeComparisonUrl = '/api/alternativeComparison';

  constructor(private http: HttpClient) { }

  getAlternatives(alternativeRequest: ChoiceRequest): Observable<ChoiceRequest> {
    return this.http.get<ChoiceRequest>(this.alternativeUrl + '/get/' + alternativeRequest['userId'] + "/1");
  }

  getAlternativeComparison(alternativeComparisonRequest: AlternativeRequest): Observable<AlternativeRequest> {
    return this.http.get<AlternativeRequest>(this.alternativeComparisonUrl + '/get/' + alternativeComparisonRequest['criteriaID'] + "/1");
  }
}
