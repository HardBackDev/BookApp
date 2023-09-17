using AutoMapper;
using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.CommentDto;
using BookAppServer.Exceptions;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BookAppServer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _maper;

        public CommentService(IRepositoryManager repository, UserManager<User> userManager, IMapper maper)
        {
            _repository = repository;
            _userManager = userManager;
            _maper = maper;
        }
        public async Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetCommentsByBook(int bookId, CommentParameters parameters)
        {
            if (await _repository.BookRepo.GetById(bookId) is null)
                throw new NotFoundException($"book with id {bookId} not found");

            var pagedResult = await _repository.CommentRepo.GetCommentsByBook(bookId, parameters);
            var comments = _maper.Map<IEnumerable<CommentDto>>(pagedResult);

            return (comments: comments, metaData: pagedResult.MetaData);
        }

        public async Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetCommentsByUser(string userName, CommentParameters parameters)
        {
            var user = await _userManager.FindByNameAsync(userName) ??
                throw new DirectoryNotFoundException($"user with name {userName} not found");

            var pagedResult = await _repository.CommentRepo.GetCommentsByUser(user.Id, parameters);
            var comments = _maper.Map<IEnumerable<CommentDto>>(pagedResult);

            return (comments: comments, metaData: pagedResult.MetaData);
        }

        public async Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetComments(CommentParameters parameters)
        {
            var pagedResult = await _repository.CommentRepo.GetAll(parameters);
            var comments = _maper.Map<IEnumerable<CommentDto>>(pagedResult);

            return (comments: comments, metaData: pagedResult.MetaData);
        }

        public async Task AddComment(int bookId, string userName, CommentForCreation commentForCreation)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var comment = new Comment()
            {
                UserId = user.Id,
                BookId = bookId,
                Text = commentForCreation.Text
            };
            _repository.CommentRepo.CreateComment(comment);
            await _repository.SaveAsync();
        }

        public async Task EditComment(int id, CommentForUpdate commentForUpdate)
        {
            var comment = await CheckCommentExistAndGet(id);
            comment.Text = commentForUpdate.Text;
            await _repository.SaveAsync();
        }

        public async Task DeleteComment(int id)
        {
            var comment = await CheckCommentExistAndGet(id);
            _repository.CommentRepo.DeleteComment(comment);
            await _repository.SaveAsync();
        }

        private async Task<Comment> CheckCommentExistAndGet(int id) =>
            await _repository.CommentRepo.GetById(id) ??
            throw new NotFoundException($"comment with id {id} not found");
    }
}
