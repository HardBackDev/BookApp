using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.CommentDto
{
    public class CommentForUpdate
    {
        [Required]
        public string Text { get; set; }
    }
}
