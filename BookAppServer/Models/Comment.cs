using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAppServer.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public string? UserId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Text { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }
    }
}
