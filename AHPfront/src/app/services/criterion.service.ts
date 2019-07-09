import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Criterion } from '../Models/Criterion';

@Injectable({
  providedIn: 'root'
})
export class CriterionService {

  apiUrl: string = 'api/criterion/';
  constructor(private http: HttpClient) { }

  GetChoiceCriteria(choiceID: string, page: number): Observable<Criterion[]> {
    return this.http.get<Criterion[]>(this.apiUrl + choiceID + '/' + page);
  }

  Add(criterion: Criterion): Observable<Criterion> {
    return this.http.post<Criterion>(this.apiUrl, criterion);
  }

  Delete(criterion: Criterion): Observable<Criterion> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: criterion,
    };
    return this.http.delete<Criterion>(this.apiUrl, options);
  }

  Update(criterion: Criterion): Observable<Criterion> {
    return this.http.put<Criterion>(this.apiUrl, criterion);
  }

}
