import { HttpResponse } from '@angular/common/http';
import { Component, } from '@angular/core';
import { Book } from 'src/app/models/book_models/book';
import { BookService } from 'src/app/services/book.service';
import { MetaData } from 'src/app/models/metadata';


@Component({
  selector: 'app-book-home',
  templateUrl: './book-home.component.html',
  styleUrls: ['./book-home.component.css']
})
export class BookHomeComponent {
  books: Book[] = [];
  currentPage: number = 1;
  forwardDisabled: boolean;
  titleFilter: string = '';
  metadata: MetaData;

  constructor(private bookService: BookService){}

  ngOnInit(){
    this.getBooksByParameters(1,"")
  }

  filterBooks(titleFilter: string, e){
    e.preventDefault();
    this.titleFilter = titleFilter
    this.getBooksByParameters(1, titleFilter)
  }

  changePage(dir: number){
    this.getBooksByParameters(this.metadata.CurrentPage + dir, this.titleFilter)
  }
  
  setPage(page: string){
    let intPage = Number.parseInt(page)
    this.getBooksByParameters(intPage, this.titleFilter)
  }

  onInputChange(event: any) {
    const inputElement = event.target;
    const settingPage: number = Number.parseInt(inputElement.value);
    if (settingPage > this.metadata.TotalPages) {
      inputElement.value = this.metadata.TotalPages
    }
    else if(settingPage <= 0){
      inputElement.value = 1
    }
  }

  getBooksByParameters(pageNumber: number, titleFilter: string) {
    this.bookService
    .getBooks(`pagenumber=${pageNumber}&titlefilter=${titleFilter}`)
    .subscribe((res: HttpResponse<Book[]>) => {
        this.books = res.body
        this.metadata = JSON.parse(res.headers.get('X-Pagination'));
    })
  }
}
