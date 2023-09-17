using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.BooksDto
{
    public class BookForAuthorCreation
    {
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Title { get; set; }
        [MaxLength(600, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Description { get; set; }
        [MaxLength(100)]
        public string? Genres { get; set; }
        public string? Photo { get; set; }
        public string? FilePath { get; set; }
    }
}
