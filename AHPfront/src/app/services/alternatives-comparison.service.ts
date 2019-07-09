import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AlternativesComparison } from '../Models/AlternativesComparison';

@Injectable({
  providedIn: 'root'
})
export class AlternativesComparisonService {

  apiUrl: string = 'api/alternativecomparison/';
  constructor(private http: HttpClient) { }

  GetAlternativeComparisons(criteriaID: string, alternativeID: string, page: number): Observable<AlternativesComparison[]> {
    return this.http.get<AlternativesComparison[]>(this.apiUrl + '/' + criteriaID + '/' + alternativeID + '/' + page);
  }

  Update(comparisons: AlternativesComparison[]): Observable<AlternativesComparison[]> {
    return this.http.put<AlternativesComparison[]>(this.apiUrl, comparisons)
  }
}
