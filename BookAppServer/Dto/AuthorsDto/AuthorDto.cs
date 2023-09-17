using BookAppServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.AuthorsDto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
    }
}
