import { NgModule } from '@angular/core';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { BookviewComponent } from './components/book-components/book-view/bookview.component';
import { BookDetailsComponent } from './components/book-components/book.details/book.details.component';
import { BookHomeComponent } from './components/book-components/book-home/book-home.component';
import { NotFoundComponent } from './components/error-pages/not-found/not-found.component';
import { AuthorListComponent } from './components/author-components/author-list/author-list.component';
import { UserRegisterComponent } from './components/user-components/user-register/user-register.component';
import { UserLoginComponent } from './components/user-components/user-login/user-login.component';
import { UserBooksComponent } from './components/user-components/user-books/user-books.component';
import { BookCreationComponent } from './components/book-components/book-creation/book-creation.component';
import { UploadComponent } from './components/upload/upload.component';
import { CookieService } from 'ngx-cookie-service';
import { AuthorDetailsComponent } from './components/author-components/author-details/author-details.component';
import { BookUpdateComponent } from './components/book-components/book-update/book-update.component';
import { BookDeleteComponent } from './components/book-components/book-delete/book-delete.component';
import { FormsModule } from '@angular/forms';
import { AuthorCreationComponent } from './components/author-components/author-creation/author-creation.component';
import { AuthorUpdateComponent } from './components/author-components/author-update/author-update.component';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}

@NgModule({
  declarations: [
    AppComponent,
    BookviewComponent,
    BookDetailsComponent,
    BookHomeComponent,
    NotFoundComponent,
    AuthorListComponent,
    UserRegisterComponent,
    UserLoginComponent,
    UserBooksComponent,
    BookCreationComponent,
    UploadComponent,
    AuthorDetailsComponent,
    BookUpdateComponent,
    BookDeleteComponent,
    AuthorCreationComponent,
    AuthorDetailsComponent,
    AuthorUpdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5173"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
