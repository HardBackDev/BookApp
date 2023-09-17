import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Author } from 'src/app/models/author_models/author';
import { MetaData } from 'src/app/models/metadata';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AuthorService } from 'src/app/services/author.service';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent {
  authors: Author[] = []
  metadata: MetaData
  _authService: AuthenticationService
  nameFilter: string = ''

  constructor(private authorService: AuthorService, private authService: AuthenticationService){
    this._authService = authService
  }

  ngOnInit(){
    this.getAuthorsByParameters(1, this.nameFilter)
  }

  deleteAuthor(id: number){
   this.authorService.deleteAuthor(id)
   .subscribe(r =>
    this.getAuthorsByParameters(1, this.nameFilter)
    ) 
  }

  filterAuthors(searchName: string, e){
    this.nameFilter = searchName
    this.getAuthorsByParameters(1,searchName)
  }

  loadNextAuthors(){
    if(this.metadata.HasNext){
      this.getAuthorsByParameters(this.metadata.CurrentPage + 1, this.nameFilter)
    }
  }

  getAuthorsByParameters(pageNumber: number, nameFilter:string ){
    this.authorService.getAuthors(this.buildQueryParameters(pageNumber, nameFilter))
    .subscribe((res: HttpResponse<Author[]>) => {
      this.authors = res.body
      this.metadata = JSON.parse(res.headers.get('X-Pagination'))
    })
  }

  buildQueryParameters(pageNumber: number, nameFilter: string):string{
    return `pageNumber=${pageNumber}&nameFilter=${nameFilter}`
  }
}
