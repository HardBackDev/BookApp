import { Author } from "../author_models/author";

export class BookDetails {
    id: number;
    title: string;
    description: string;
    genres: string;
    authorId: number;
    photo: string;
    filePath: string;
    author: Author;
  }