import { Injectable } from '@angular/core';
import { enviroment } from '../enviroments/enviroment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { CommentForCreation } from '../models/comment_models/comment-creation';
import { CommentForEdit } from '../models/comment_models/comment-edit';
import { Comment } from '../models/comment_models/comment';


@Injectable({
  providedIn: 'root'
})
export class CommentService {
  commentUrl: string = `${enviroment.apiUrl}/comments`;

  constructor( private httpContext: HttpClient) { }
  
  public getBookComments(bookId: number, params: string) : Observable<HttpResponse<Comment[]>> {
    return this.httpContext.get<Comment[]>(`${this.commentUrl}/ByBook/${bookId}?${params}`, { observe: 'response' })
  }

  public getCommentsByUser(params: string) : Observable<HttpResponse<Comment[]>> {
    return this.httpContext.get<Comment[]>(`${this.commentUrl}/GetByUser?${params}`, { headers: this.generateHeaders(), observe: 'response' })
  }

  public addComment(bookId: number, comment: CommentForCreation) {
    return this.httpContext.post(`${this.commentUrl}/${bookId}`, comment, {headers: this.generateHeaders()})
  }

  public editComment(commentId: number, comment: CommentForEdit){
    return this.httpContext.put(`${this.commentUrl}/${commentId}`, comment, {headers: this.generateHeaders()})
  }

  public deleteComment(commentId: number){
    return this.httpContext.delete(`${this.commentUrl}/${commentId}`, {headers: this.generateHeaders()})
  }

  private generateHeaders = () => {
    return new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem("jwt")}`
    })
  }
}
