import { Injectable } from '@angular/core';
import { enviroment } from '../enviroments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Author } from '../models/author_models/author';
import { Observable } from 'rxjs/internal/Observable';
import { AuthorDetails } from '../models/author_models/author-details';
import { AuthorForCreation } from '../models/author_models/author-for-creation';
import { AuthorForUpdate } from '../models/author_models/author-for-update';
import { Book } from '../models/book_models/book';

@Injectable({
  providedIn: 'root'
})

export class AuthorService {
  authorUrl: string = `${enviroment.apiUrl}/authors`
  constructor( private httpContext: HttpClient) { }
  
  public getAuthors(parameters: string) : Observable<HttpResponse<Author[]>> {
    return this.httpContext.get<Author[]>(`${this.authorUrl}?${parameters}`, {headers: this.generateHeaders(), observe: 'response'})
  }

  public getAuthor(id:number) : Observable<AuthorDetails>{
    return this.httpContext.get<AuthorDetails>(`${this.authorUrl}/${id}`, {headers: this.generateHeaders()})
  }

  public getAuthorBooks(id:number, parameters:string) : Observable<HttpResponse<Book[]>> {
    return this.httpContext.get<Book[]>(`${this.authorUrl}/${id}/books?${parameters}`, { observe: 'response' })
  }

  public createAuthor(author:AuthorForCreation) {
    return this.httpContext.post(`${this.authorUrl}`, author, {headers: this.generateHeaders()})
  }

  public updateAuthor(id:number, author:AuthorForUpdate) {
    return this.httpContext.put(`${this.authorUrl}/${id}`, author, {headers: this.generateHeaders()})
  }

  public deleteAuthor(id:number) {
    return this.httpContext.delete(`${this.authorUrl}/${id}`, {headers: this.generateHeaders()})
  }

  private generateHeaders = () => {
    return  new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('jwt')}`
       })
  }
}
