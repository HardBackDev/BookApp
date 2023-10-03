import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { tokenGetter } from '../app.module';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard {

  constructor(private router:Router, private jwtHelper: JwtHelperService){}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    const decodedToken = this.jwtHelper.decodeToken(tokenGetter());

    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    return role == "Admin"
  }
}