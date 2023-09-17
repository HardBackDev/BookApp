import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthorForCreation } from 'src/app/models/author_models/author-for-creation';
import { AuthorService } from 'src/app/services/author.service';

@Component({
  selector: 'app-author-creation',
  templateUrl: './author-creation.component.html',
  styleUrls: ['./author-creation.component.css']
})
export class AuthorCreationComponent {
  authorForm: FormGroup;

  constructor(private authorService: AuthorService, private router: Router) { }

  ngOnInit(): void {
    this.authorForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      bio: new FormControl('', [Validators.required])
    });
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

  createAuthor = (bookForm) => {
    if (this.authorForm.valid)
      this.executeAuthorCreation(bookForm);
  }

  private executeAuthorCreation = (authorForm) => {
    
      const author: AuthorForCreation = {
        name: authorForm.name,
        bio: authorForm.bio
      }

      this.authorService.createAuthor(author)
      .subscribe(r => this.redirectToAuthors());
  }

  redirectToAuthors = () => {
    this.router.navigate(['authors']);
  }
}
