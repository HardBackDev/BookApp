using BookAppServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.CommentDto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Text { get; set; }
    }
}
