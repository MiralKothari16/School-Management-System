import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../auth/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private auth: AuthenticationService, private route: Router) { }
  //  canActivate(
  //    route: ActivatedRouteSnapshot,
  //    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
  //    return true;
  //  }
  canActivate(): boolean {
    if (this.auth.isLoggedIn()) { return true; }
    else {
      this.route.navigate(['login']);
      return false;
    }
  }
}


