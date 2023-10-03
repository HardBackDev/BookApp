import { Component, Input } from '@angular/core';
import { Author } from 'src/app/models/author_models/author';
import { Book } from 'src/app/models/book_models/book';
import { AuthorService } from 'src/app/services/author.service';


@Component({
  selector: 'app-bookview',
  templateUrl: './bookview.component.html',
  styleUrls: ['./bookview.component.css'],
})
export class BookviewComponent {
  @Input() book!: Book;
  author: Author

  constructor(private authorService: AuthorService){

  }

  ngOnInit(){
    this.authorService.getAuthor(this.book.authorId)
    .subscribe((res: Author) => {
      this.author = res
    })
  }
}
