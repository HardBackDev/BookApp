using BookAppServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.BooksDto
{
    public class BookForCreation
    {
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Title { get; set; }
        [MaxLength(600, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Description { get; set; }
        [MaxLength(100)]
        public string? Genres { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public string? Photo { get; set; }
        public string? FilePath { get; set; }
    }
}
