import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Choice } from '../Models/Choice';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ChoiceService {
  apiUrl: string = 'api/choice/';
  constructor(private http: HttpClient) { }

  GetUserChoices(userID: string, page: number): Observable<Choice[]> {

    return this.http.get<Choice[]>(this.apiUrl + userID + '/' + page);
    
  }

  Add(choice: Choice): Observable<Choice> {
    return this.http.post<Choice>(this.apiUrl, choice);
  }

  Delete(choice: Choice): Observable<Choice> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: choice,
    };
    return this.http.delete<Choice>(this.apiUrl, options);
  }

  Update(choice: Choice): Observable<Choice> {
    return this.http.put<Choice>(this.apiUrl, choice);
  }

  Calculate(choiceID: string): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl + 'calculate/' + choiceID);
  }


}
