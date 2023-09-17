import { Injectable } from '@angular/core';
import { enviroment } from '../enviroments/enviroment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Book } from '../models/book_models/book';
import { Observable } from 'rxjs/internal/Observable';
import { BookForCreation } from '../models/book_models/book-creation';
import { BookDetails } from '../models/book_models/book-details';
import { BookForUpdate } from '../models/book_models/book-update';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  bookUrl: string = `${enviroment.apiUrl}/books`;

  constructor( private httpContext: HttpClient) { }
  
  public getBooks(params: string) : Observable<HttpResponse<Book[]>> {
    return this.httpContext.get<Book[]>(`${this.bookUrl}?${params}`, { observe: 'response' })
  }

  public getBook(id:number) : Observable<BookDetails>{
    return this.httpContext.get<BookDetails>(`${this.bookUrl}/${id}`, this.generateHeaders())
  }

  public getBooksByAuthor(authorId:number) : Observable<Book[]>{
    return this.httpContext.get<Book[]>(`${this.bookUrl}/ByAuthor/${authorId}`, this.generateHeaders())
  }
  
  public createBook = (book: BookForCreation) => {
    return this.httpContext.post<Book>(`${this.bookUrl}`, book, this.generateHeaders())
  }

  public updateBook = (id: number,book: BookForUpdate) => {
    return this.httpContext.put(`${this.bookUrl}/${id}`, book, this.generateHeaders())
  }

  public deleteBook = (id: number) => {
    return this.httpContext.delete(`${this.bookUrl}/${id}`, this.generateHeaders())
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('jwt')}`})
    }
  }
}
