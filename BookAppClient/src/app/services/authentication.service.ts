import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from '../enviroments/enviroment';
import { Book } from '../models/book_models/book';
import { UserRegister } from '../models/user_models/user-register';
import { UserForLogin } from '../models/user_models/user-for-login';
import { JwtToken } from '../models/jwt-token';
import { CookieService } from 'ngx-cookie-service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  authUrl: string = `${enviroment.apiUrl}/auth`
  constructor( private httpContext: HttpClient, private jwtHelper: JwtHelperService) { }
  
  public registerUser = (user: UserRegister) => {
    return this.httpContext.post<UserRegister>(this.authUrl, user, this.generateHeaders());
  }

  public loginUser = (user: UserForLogin) => {
    return this.httpContext.post<JwtToken>(`${this.authUrl}/login`, user, this.generateHeaders());
  }

  public getRole = () => {
    if(!this.isUserAuthenticated())
      return null
    const decodedToken = this.jwtHelper.decodeToken(localStorage.getItem('jwt'));
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    return role
  }

  public getUserName = () => {
    if(!this.isUserAuthenticated())
      return null
      const decodedToken = this.jwtHelper.decodeToken(localStorage.getItem('jwt'));
      const userName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
      return userName
      
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("jwt");
    
    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    return false;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('jwt')}` 
      })
    }
  }
}
