import { Component, inject, HostListener, ElementRef, Renderer2 } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { ActivatedRoute } from '@angular/router';
import { FileService } from 'src/app/services/file.service';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { BookDetails } from 'src/app/models/book_models/book-details';
import { Author } from 'src/app/models/author_models/author';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Comment } from 'src/app/models/comment_models/comment';
import { CommentForEdit } from 'src/app/models/comment_models/comment-edit';
import { CommentService } from 'src/app/services/comment.service';
import { CommentForCreation } from 'src/app/models/comment_models/comment-creation';
import { MetaData } from 'src/app/models/metadata';
import { UserService } from 'src/app/services/user.service';
import { NgxSpinner } from 'ngx-spinner';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-book.details',
  templateUrl: './book.details.component.html',
  styleUrls: ['./book.details.component.css']
})
export class BookDetailsComponent {
  route: ActivatedRoute = inject(ActivatedRoute);
  book: BookDetails;
  message: string;
  progress: number;
  author: Author;
  comments: Comment[];
  metaData: MetaData;
  bookId: number;
  commentText: string;
  editingComment: number = -1;
  _authService: AuthenticationService
  
  constructor(private bookService: BookService, private fileService: FileService, private userService: UserService,
     private authService: AuthenticationService, private commentService: CommentService){
      this._authService = authService
      window.addEventListener('scroll', this.scroll);
     }

  ngOnInit(){
    this.bookId = Number(this.route.snapshot.params['id']);
    this.bookService
    .getBook(this.bookId)
    .subscribe((res: BookDetails) =>{
      this.book = res;
    })
    
    this.commentService
    .getBookComments(this.bookId, 'pageSize=6')
    .subscribe((res: HttpResponse<Comment[]>) =>{
      this.comments = res.body;
      this.metaData = JSON.parse(res.headers.get('X-Pagination'));
    })
    
  }

  scroll = () => {
    if (this.isScrolledToBottom()) {
      this.loadNextCommentts();
    }
  };

  isScrolledToBottom(): boolean {
    const windowHeight = window.innerHeight;
    const scrollY = window.scrollY;
    const bodyHeight = document.body.clientHeight;
    
    return windowHeight + scrollY >= bodyHeight - 54;
  }

  loadNextCommentts(): void {
    if(this.metaData.CurrentPage + 1 <= this.metaData.TotalPages){
      this.getNextComments()
    }
  }

  getNextComments(){
    this.commentService
    .getBookComments(this.bookId, `pageNumber=${this.metaData.CurrentPage + 1}`)
    .subscribe((res: HttpResponse<Comment[]>) =>{

      const newComments: Comment[] = res.body
      this.comments = this.comments.concat(newComments);
      this.metaData = JSON.parse(res.headers.get('X-Pagination'));
    })
  }
  
  //#region 
  download = (fileUrl: string) => {
    this.fileService.download(fileUrl)
    .subscribe((event) => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round((100 * event.loaded) / event.total);
      else if (event.type === HttpEventType.Response) {
        this.message = 'Download success.';
      this.downloadFile(event, fileUrl);
      }
    });
  }

  private downloadFile = (data: HttpResponse<Blob>, fileUrl: string) => {
    const downloadedFile = new Blob([data.body], { type: data.body.type });
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = fileUrl;
    a.href = URL.createObjectURL(downloadedFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
  }

  addBookToMyFavoriteBooks(){
    this.userService.addToFavoriteBook(this.bookId)
    .subscribe()
  }

  addComment(text: string, e){
    e.preventDefault();
    const comment : CommentForCreation = {
      text: text
    }
    
    this.commentService.addComment(this.bookId, comment)
    .subscribe(res => window.location.reload())
  }

  editComment(commentId: number, textForUpdate: string, e){
    e.preventDefault();

    const commentForEdit: CommentForEdit = {
      text: textForUpdate
    }
    
    this.commentService.editComment(commentId, commentForEdit)
    .subscribe(res => window.location.reload());
  }
  
  openOrCloseOptions(element: HTMLDialogElement){
    if(element.open === true)
      element.open = false
    else
      element.open = true
  }

  isEditingComment(commentId: number) : boolean{
    if(this.editingComment === commentId)
      return true
    else
      return false
  }

  openEdit(commentId: number){
    this.editingComment = commentId
  }

  deleteComment(commentId: number){
    this.commentService.deleteComment(commentId)
    .subscribe(res => window.location.reload())
  }

  //#endregion
}
