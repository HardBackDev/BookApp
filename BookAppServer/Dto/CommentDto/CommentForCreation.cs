using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.CommentDto
{
    public class CommentForCreation
    {
        [Required]
        public string Text { get; set; }
    }
}
