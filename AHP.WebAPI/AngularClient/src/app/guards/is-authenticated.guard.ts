import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { HelperFunctions } from '../classes/helper-functions';

@Injectable({
  providedIn: 'root'
})


export class IsAuthenticatedGuard implements CanActivate {
  constructor(private router: Router, private helper: HelperFunctions) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {

    if (!this.helper.isAuthenticated()) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }

}
