import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Book } from 'src/app/models/book_models/book';


@Component({
  selector: 'app-bookview',
  templateUrl: './bookview.component.html',
  styleUrls: ['./bookview.component.css'],
})
export class BookviewComponent {
  @Input() book!: Book;

}
