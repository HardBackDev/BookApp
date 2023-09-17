import { Book } from "../book_models/book";

export class AuthorDetails {
    id: number;
    name: string;
    bio: string;
    books: Book[];
  }