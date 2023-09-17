import { Component, Input, inject } from '@angular/core';
import { Author } from 'src/app/models/author_models/author';
import { Book } from 'src/app/models/book_models/book';
import { BookService } from 'src/app/services/book.service';
import { ActivatedRoute } from '@angular/router';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { AuthorDetails } from 'src/app/models/author_models/author-details';
import { AuthorService } from 'src/app/services/author.service';

@Component({
  selector: 'app-author-details',
  templateUrl: './author-details.component.html',
  styleUrls: ['./author-details.component.css']
})
export class AuthorDetailsComponent {

  author: AuthorDetails
  currentPage: number = 1;
  forwardDisabled: boolean;
  books: Book[] = []
  route: ActivatedRoute = inject(ActivatedRoute);

  constructor(private authorService: AuthorService){
  }

  ngOnInit(){
    const id = Number(this.route.snapshot.params['id']);
    this.authorService
    .getAuthor(id)
    .subscribe((res: AuthorDetails) =>{
      this.author = res
    })
    this.authorService
      .getAuthorBooks(id, "pagenumber=1")
      .subscribe(res => this.books = res.body)
  }
}
