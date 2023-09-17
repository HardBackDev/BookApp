using BookAppServer.Dto.BooksDto;
using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.AuthorsDto
{
    public class AuthorForUpdate
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }
        [MaxLength(500, ErrorMessage = "Maximum length for the Bio is 600 characters.")]
        public string? Bio { get; set; }
        public IEnumerable<BookForAuthorCreation>? Books { get; set; }
    }
}
