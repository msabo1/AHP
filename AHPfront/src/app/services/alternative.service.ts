import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Alternative } from '../Models/Alternative';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AlternativeService {
  apiUrl: string = 'api/alternative/';
  constructor(private http: HttpClient) { }

  GetChoiceAlternatives(alternativeID: string, page: number): Observable<Alternative[]> {
    return this.http.get<Alternative[]>(this.apiUrl + alternativeID + '/' + page);
  }

  Add(alternative: Alternative): Observable<Alternative> {
    return this.http.post<Alternative>(this.apiUrl, alternative);
  }

  Delete(alternative: Alternative): Observable<Alternative> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: alternative,
    };
    return this.http.delete<Alternative>(this.apiUrl, options);
  }

  Update(alternative: Alternative): Observable<Alternative> {
    return this.http.put<Alternative>(this.apiUrl, alternative);
  }
}
