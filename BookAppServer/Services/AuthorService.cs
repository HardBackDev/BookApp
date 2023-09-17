using AutoMapper;
using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.AuthorsDto;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Exceptions;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net;

namespace BookAppServer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public AuthorService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<AuthorDto> authors, MetaData metaData)> GetAllAuthors(AuthorParameters authorParameters)
        {
            var pagedResult = await _repository.AuthorRepo.GetAll(authorParameters);
            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(pagedResult);
            return (authors: authorsDto, metaData: pagedResult.MetaData);
        }

        public async Task<AuthorDto> GetAuthor(int id)
        {
            var author = await _repository.AuthorRepo.GetById(id);
            var authorById = _mapper.Map<AuthorDto>(author);
            return authorById;
        }

        public async Task<AuthorDto> CreateAuthor(AuthorForCreation authorForCreation)
        {
            var author = _mapper.Map<Author>(authorForCreation);
            author.Books?.Select(b => b.AuthorId = author.Id).ToList();
            _repository.AuthorRepo.CreateAuthor(author);
            await _repository.SaveAsync();
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task CreateBooksForAuthor(int authorId, IEnumerable<BookForAuthorCreation> books)
        {
            await CheckAuthorExistAndGet(authorId);
            var booksForCreation = _mapper.Map<IEnumerable<Book>>(books);

            foreach (var book in booksForCreation)
            {
                book.AuthorId = authorId;
                _repository.BookRepo.CreateBook(book);
            }
            await _repository.SaveAsync();
        }

        public async Task UpdateAuthor(int id, AuthorForUpdate authorForUpdate)
        {
            await CheckAuthorExistAndGet(id);
            var author = await _repository.AuthorRepo.GetById(id);
            _mapper.Map(authorForUpdate, author);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetBooksByAuthor(int authorId, BookParameters bookParameters)
        {
            await CheckAuthorExistAndGet(authorId);
            var pagedResult = await _repository.AuthorRepo.GetBooksByAuthor(authorId, bookParameters);
            var books = _mapper.Map<IEnumerable<BookDto>>(pagedResult);
            return (books: books, metaData: pagedResult.MetaData);
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await CheckAuthorExistAndGet(id);

            _repository.AuthorRepo.DeleteAuthor(author);
            await _repository.SaveAsync();
        }


        private async Task<Author> CheckAuthorExistAndGet(int authorId) =>
            await _repository.AuthorRepo.GetById(authorId) ??
                throw new NotFoundException($"author with id:{authorId} dont exist");

    }
}