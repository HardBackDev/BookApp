import { HttpClient, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BookForCreation } from 'src/app/models/book_models/book-creation';
import { UserRegister } from 'src/app/models/user_models/user-register';
import { BookService } from 'src/app/services/book.service';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-book-creation',
  templateUrl: './book-creation.component.html',
  styleUrls: ['./book-creation.component.css']
})
export class BookCreationComponent {
  bookForm: FormGroup;
  selectedFile: File;

  constructor(private bookService: BookService, private router: Router, private fileService: FileService) { }

  filePath: { path: '' }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    this.selectedFile.arrayBuffer();
  }

  ngOnInit(): void {
    this.bookForm = new FormGroup({
      title: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      authorId: new FormControl(null, [Validators.required]),
      genres: new FormControl(''),
      photo: new FormControl('')
    });
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

  createBook = (bookForm) => {
    if (this.bookForm.valid)
      this.executeBookCreation(bookForm);
  }

  private executeBookCreation = (bookForm) => {
    
      const book: BookForCreation = {
        title: bookForm.title,
        authorId: bookForm.authorId,
        description: bookForm.description,
        photo: bookForm.photo,
        genres: bookForm.genres,
        filePath: this.filePath.path
      }

      this.bookService.createBook(book)
      .subscribe(r => this.redirectToBooks());
  }

  uploadFinished = (event) => { 
    console.log(event)
    this.filePath = event; 
  }

  redirectToBooks = () => {
    this.router.navigate(['']);
  }

  progress: number;
  message: string;
  isUploading: boolean = false;

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
