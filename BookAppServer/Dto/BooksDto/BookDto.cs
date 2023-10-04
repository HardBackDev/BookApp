using BookAppServer.Dto.AuthorsDto;
using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.BooksDto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Genres { get; set; }
        public int AuthorId { get; set; }
        public string? Photo { get; set; }
        public string? FilePath { get; set; }
        public AuthorDto? Author { get; set; }
    }
}
