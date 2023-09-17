import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Book } from 'src/app/models/book_models/book';
import { MetaData } from 'src/app/models/metadata';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-books',
  templateUrl: './user-books.component.html',
  styleUrls: ['./user-books.component.css']
})

export class UserBooksComponent {
  books: Book[] = []
  metadata: MetaData
  titleFilter: string
  constructor(private userSerivce: UserService) { }

  ngOnInit(){
    this.userSerivce.getUserBooks("pageNumber=1")
    .subscribe((result: HttpResponse<Book[]>) =>{
      this.books = result.body
      this.metadata = JSON.parse(result.headers.get('X-pagination'))
    });
  
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
    this.getBooksByParameters(Number.parseInt(page), this.titleFilter)
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
    this.userSerivce
    .getUserBooks(`pagenumber=${pageNumber}&titlefilter=${titleFilter}`)
    .subscribe((res: HttpResponse<Book[]>) => {
        this.books = res.body
        this.metadata = JSON.parse(res.headers.get('X-Pagination'));
    })
  }
}
