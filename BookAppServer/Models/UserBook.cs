using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAppServer.Models
{
    public class UserBook
    {
        [Column("UserBookId")]
        [Key]
        public int Id { get; set; }
        
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book? Book { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
