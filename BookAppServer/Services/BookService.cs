using AutoMapper;
using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Exceptions;
using BookAppServer.Filters;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace BookAppServer.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public BookService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllBooks(BookParameters bookParameters)
        {
            var pagedResult = await _repository.BookRepo.GetAll(bookParameters);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(pagedResult);
            return (books: booksDto, metaData: pagedResult.MetaData);
        }

        public async Task<BookDto> GetBook(int id) => _mapper.Map<BookDto>(await CheckBookExistAndGet(id));

        public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetBooksByAuthor(int authorId, BookParameters bookParameters)
        {
            await CheckAuthorExist(authorId);
            var pagedResult = await _repository.BookRepo.GetBooksByAuthor(authorId, bookParameters);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(pagedResult);

            return (books: booksDto, metaData: pagedResult.MetaData);
        }

        public async Task<BookDto> CreateBook(BookForCreation bookForCreation)
        {
            await CheckAuthorExist(bookForCreation.AuthorId);
            var book = _mapper.Map<Book>(bookForCreation);
            _repository.BookRepo.CreateBook(book);
            await _repository.SaveAsync();
            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<Book>> CreateBooks(IEnumerable<BookForCreation> booksForCreation)
        {
            List<Book> booksForReturn = new List<Book>();
            foreach (var bookForCreation in booksForCreation)
            {
                var book = _mapper.Map<Book>(bookForCreation);
                _repository.BookRepo.CreateBook(book);
                booksForReturn.Add(book);
            }
            await _repository.SaveAsync();
            return booksForReturn;
        }

        public async Task UpdateBook(int bookId, BookForUpdate bookForUpdate)
        {
            await CheckBookExistAndGet(bookId);
            await CheckAuthorExist(bookForUpdate.AuthorId);
            var book = await _repository.BookRepo.GetById(bookId);
            
            _mapper.Map(bookForUpdate, book);
            await _repository.SaveAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await CheckBookExistAndGet(id);
            if(File.Exists(book.FilePath))
                File.Delete(book.FilePath);
            _repository.BookRepo.DeleteBook(book);
            await _repository.SaveAsync();
        }

        private async Task CheckAuthorExist(int id)
        {
            if(await _repository.AuthorRepo.GetById(id) is null)
                throw new NotFoundException($"author with id:{id} not found");
        }
        
        private async Task<Book> CheckBookExistAndGet(int id) =>  
            await _repository.BookRepo.GetById(id) ??
                throw new NotFoundException($"book with id:{id} not found");
    }
}
