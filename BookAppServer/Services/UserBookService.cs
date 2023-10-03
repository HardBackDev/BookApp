using AutoMapper;
using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Exceptions;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace BookAppServer.Services
{
    public class UserBookService : IUserBookService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserBookService(IMapper mapper, IRepositoryManager repository, UserManager<User> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task AddToFavoriteBooks(int bookId, string userName)
        {
            if(await _repository.BookRepo.GetById(bookId) is null)
                throw new NotFoundException($"Book with id: {bookId}, not found");

            var user = await _userManager.FindByNameAsync(userName) ??
                throw new NotFoundException($"User with Name: {userName}, not found");

            _repository.UserBookRepo.AddUserBook(bookId, user.Id);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetUserBooks(string userName, BookParameters parameters)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var pagedResult = await _repository.UserBookRepo.GetUserBooks(user.Id, parameters);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(pagedResult);

            return (books: booksDto, metaData: pagedResult.MetaData);
        }

        public async Task<bool> CheckBookInFavorites(string userName, int bookId)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _repository.UserBookRepo.CheckBookInFavorites(user.Id, bookId);

            return result;
        }
    }
}
