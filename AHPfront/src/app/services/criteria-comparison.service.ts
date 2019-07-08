import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CriteriaComparison } from '../Models/CriteriaComparison';

@Injectable({
  providedIn: 'root'
})
export class CriteriaComparisonService {

  apiUrl: string = 'api/criteriacomparison/';
  constructor(private http: HttpClient) { }

  Add(comparisons: CriteriaComparison[]): Observable<CriteriaComparison[]> {
    return this.http.post<CriteriaComparison[]>(this.apiUrl, comparisons);
  }
}
