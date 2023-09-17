import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { BookHomeComponent } from './components/book-components/book-home/book-home.component';
import { NotFoundComponent } from './components/error-pages/not-found/not-found.component';
import { BookDetailsComponent } from './components/book-components/book.details/book.details.component';
import { AuthorListComponent } from './components/author-components/author-list/author-list.component';
import { UserRegisterComponent } from './components/user-components/user-register/user-register.component';
import { UserLoginComponent } from './components/user-components/user-login/user-login.component';
import { UserBooksComponent } from './components/user-components/user-books/user-books.component';
import { AuthGuard } from './guards/auth.guard';
import { BookCreationComponent } from './components/book-components/book-creation/book-creation.component';
import { AuthorDetailsComponent } from './components/author-components/author-details/author-details.component';
import { BookUpdateComponent } from './components/book-components/book-update/book-update.component';
import { BookDeleteComponent } from './components/book-components/book-delete/book-delete.component';
import { AuthorCreationComponent } from './components/author-components/author-creation/author-creation.component';
import { AuthorUpdateComponent } from './components/author-components/author-update/author-update.component';


const routes: Routes = [
  { path: 'register', component: UserRegisterComponent},
  { path: 'login', component: UserLoginComponent},
  { path: 'userbooks', component: UserBooksComponent, canActivate: [AuthGuard] },
  { path: 'authors', component: AuthorListComponent},
  { path: 'authors/details/:id', component: AuthorDetailsComponent},
  { path: 'authors/create', component: AuthorCreationComponent},
  { path: 'authors/update/:id', component: AuthorUpdateComponent},
  { path: '', component:  BookHomeComponent},
  { path: 'books/details/:id', component: BookDetailsComponent },
  { path: 'books/update/:id', component: BookUpdateComponent },
  { path: 'books/delete/:id', component: BookDeleteComponent },
  { path: 'addbook', component: BookCreationComponent},
  { path: '404', component: NotFoundComponent }, 
  { path: '**', redirectTo: '/404', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
