import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from 'src/app/models/book_models/book';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-delete',
  templateUrl: './book-delete.component.html',
  styleUrls: ['./book-delete.component.css']
})
export class BookDeleteComponent {
  book: Book;

  constructor(private bookService: BookService, private activeRoute: ActivatedRoute, private router: Router){}

  ngOnInit(){
    this.getBookById()
  }

  getBookById(){
    const bookId: number = Number(this.activeRoute.snapshot.params['id']);

    this.bookService.getBook(bookId)
    .subscribe((res: Book) => this.book = res)
  }

  redirectToBookList = () => {
    this.router.navigate(['/']);
  }

  deleteBook(){
    this.bookService.deleteBook(this.book.id)
    .subscribe(res => this.redirectToBookList())
  }
}
