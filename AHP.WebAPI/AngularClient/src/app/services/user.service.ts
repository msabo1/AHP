import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpClient} from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { User } from '../classes/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userUrl = '/api/user';

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
}
