using BookAppServer.Dto.AuthorsDto;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;

namespace BookAppServer.Contracts.ServicesContracts
{
    public interface IAuthorService
    {
        Task<(IEnumerable<AuthorDto> authors, MetaData metaData)> GetAllAuthors(AuthorParameters authorParameters);
        Task<AuthorDto> GetAuthor(int id);
        Task<AuthorDto> CreateAuthor(AuthorForCreation authorForCreation);
        Task CreateBooksForAuthor(int authorId, IEnumerable<BookForAuthorCreation> books);
        Task UpdateAuthor(int id, AuthorForUpdate authorForUpdate);
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetBooksByAuthor(int authorId, BookParameters bookParameters);
        public Task DeleteAuthor(int id);

    }
}
