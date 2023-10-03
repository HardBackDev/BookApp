import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from '../enviroments/environment';
import { Book } from '../models/book_models/book';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../models/user_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  userUrl: string = `${enviroment.apiUrl}/userbooks`
  constructor( private httpContext: HttpClient, private cookieService: CookieService) { }
  
  public getUser(id: string) : Observable<User> {
    return this.httpContext.get<User>(`${this.userUrl}/${id}`, {headers: this.generateHeaders()});
  }

  public getUserBooks(parameters: string) : Observable<HttpResponse<Book[]>> {
    return this.httpContext.get<Book[]>(`${this.userUrl}?${parameters}`, {observe: 'response', headers: this.generateHeaders()});
  }

  public checkBookInFavorites(bookId: number) : Observable<boolean> {
    return this.httpContext.get<boolean>(`${this.userUrl}/${bookId}`, {headers: this.generateHeaders()})
  }

  public addToFavoriteBook(id: number) : Observable<Book[]> {
    return this.httpContext.post<Book[]>(`${this.userUrl}/${id}`, null, {headers: this.generateHeaders()});
  }

  private generateHeaders = () => {
    return new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem("jwt")}`
       })
  }
}
