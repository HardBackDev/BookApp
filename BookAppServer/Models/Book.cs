using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookAppServer.Models
{
    public class Book
    {
        [Key]
        [Column("BookId")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Title { get; set; }
        [MaxLength(2000, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Description { get; set; }
        public string? Genres { get; set; }
        public string? Photo { get; set; }
        public string? FilePath { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        [JsonIgnore]
        public Author? Author { get; set; }
    }
}
