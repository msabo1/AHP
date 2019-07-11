import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CriteriaRequest } from '../classes/criteria-request';
import { CriteriaComparison } from '../classes/criteria-comparison';
import { Criteria } from '../classes/criteria';

@Injectable({
  providedIn: 'root'
})
export class CriteriaService {

  private criterionUrl = '/api/criterion';
  private criteriaComparisonUrl = '/api/criteriaComparison';

  constructor(private http: HttpClient) { }

  getCriteria(criteriaRequest: CriteriaRequest): Observable<CriteriaRequest> {
    return this.http.get<CriteriaRequest>(this.criterionUrl + '/get/' + criteriaRequest['choiceId'] + "/1");
  }

  getCriteriaComparison(criteriaComparisonRequest: CriteriaRequest): Observable<CriteriaRequest> {
    return this.http.get<CriteriaRequest>(this.criteriaComparisonUrl + '/get/' + criteriaComparisonRequest['choiceId'] + "/1");
  }

  addCriteria(criteria: Criteria[]): Observable<Criteria[]> {
    return this.http.post<Criteria[]>(this.criterionUrl + '/postCriteria/', criteria);
  }
  addCriteriaComparisons(criteriaComparisons: CriteriaComparison[]): Observable<CriteriaComparison> {
    return this.http.post<CriteriaComparison>(this.criteriaComparisonUrl + '/postCriteriaComparisons/', criteriaComparisons);
  }

}
