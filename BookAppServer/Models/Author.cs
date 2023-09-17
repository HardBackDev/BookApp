using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookAppServer.Dto.BooksDto;

namespace BookAppServer.Models
{
    public class Author
    {
        [Column("AuthorId")]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }
        [MaxLength(2000, ErrorMessage = "Maximum length for the Bio is 600 characters.")]
        public string? Bio { get; set; }
        public List<Book>? Books { get; set; }
    }
}
