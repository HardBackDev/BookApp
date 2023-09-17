import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Author } from 'src/app/models/author_models/author';
import { AuthorForUpdate } from 'src/app/models/author_models/author-for-update';
import { Book } from 'src/app/models/book_models/book';
import { AuthorService } from 'src/app/services/author.service';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-author-update',
  templateUrl: './author-update.component.html',
  styleUrls: ['./author-update.component.css']
})
export class AuthorUpdateComponent {
  id: number
  route: ActivatedRoute = inject(ActivatedRoute);
  authorForm: FormGroup;
  author: Author;

  constructor(private authorService: AuthorService, private router: Router){}

  ngOnInit() : void{
    this.id = Number(this.route.snapshot.params['id']);
    this.authorForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      bio: new FormControl('', [Validators.required])
    });

    this.getAuthorById();
  }

  private getAuthorById = () => {

    this.authorService.getAuthor(this.id)
    .subscribe((result: Author) => {
        this.author = result;
        this.authorForm.patchValue(this.author);
      })
  }

  validateControl = (controlName: string) => {
    if (this.authorForm.get(controlName).invalid && this.authorForm.get(controlName).touched)
      return true;
    
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.authorForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  updateAuthor = (authorForm) => {
    if (this.authorForm.valid)
      this.executeAuthorUpdate(authorForm);
  }

  private executeAuthorUpdate = (authorForm) => {
    
      const author: AuthorForUpdate = {
        name: authorForm.name,
        bio: authorForm.bio
      }

      this.authorService.updateAuthor(this.id, author)
      .subscribe(res => this.redirectToAuthors() );
  }

  redirectToAuthors = () => {
    this.router.navigate(['authors']);
  }

}
