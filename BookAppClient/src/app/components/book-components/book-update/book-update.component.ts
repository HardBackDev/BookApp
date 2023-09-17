import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from 'src/app/models/book_models/book';
import { BookForCreation } from 'src/app/models/book_models/book-creation';
import { BookForUpdate } from 'src/app/models/book_models/book-update';
import { BookService } from 'src/app/services/book.service';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-book-update',
  templateUrl: './book-update.component.html',
  styleUrls: ['./book-update.component.css']
})
export class BookUpdateComponent {
  id: number
  route: ActivatedRoute = inject(ActivatedRoute);
  bookForm: FormGroup;
  book: Book;
  selectedFile: File;
  progress: number;
  message: string;
  isUploading: boolean = false;
  filePath: { path: '' } = { path: '' };

  constructor(private bookService: BookService, private fileService: FileService, private router: Router){}

  ngOnInit() : void{
    this.id = Number(this.route.snapshot.params['id']);
    this.bookForm = new FormGroup({
      title: new FormControl(''),
      description: new FormControl(''),
      authorId: new FormControl(null),
      genres: new FormControl(''),
      photo: new FormControl('')
    });

    this.getBookById();
  }

  private getBookById = () => {

    this.bookService.getBook(this.id)
    .subscribe({
      next: (result: Book) => {
        this.book = result;
        this.bookForm.patchValue(this.book);
      },
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    this.selectedFile.arrayBuffer();
  }

  validateControl = (controlName: string) => {
    if (this.bookForm.get(controlName).invalid && this.bookForm.get(controlName).touched)
      return true;
    
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.bookForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  updateBook = (bookForm) => {
    if (this.bookForm.valid)
      this.executeBookUpdate(bookForm);
  }

  private executeBookUpdate = (bookForm) => {
    
      const book: BookForUpdate = {
        title: bookForm.title,
        authorId: bookForm.authorId,
        description: bookForm.description,
        photo: bookForm.photo,
        genres: bookForm.genres,
        filePath: this.filePath.path ?? ''
      }

      this.bookService.updateBook(this.id, book)
      .subscribe(res => this.redirectToBooks() );
  }

  uploadFinished = (event) => { 
    console.log(event)
    this.filePath = event; 
  }

  redirectToBooks = () => {
    this.router.navigate(['']);
  }


  uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    
    this.fileService.uploadFile(formData)
      .subscribe({
        next: (event) => {
        if (event.type === HttpEventType.UploadProgress){
          this.progress = Math.round(100 * event.loaded / event.total);
          this.isUploading = false;
        }
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.uploadFinished(event.body);
          this.isUploading = true;
        }
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

}
