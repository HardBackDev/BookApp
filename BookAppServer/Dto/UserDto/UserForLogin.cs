using System.ComponentModel.DataAnnotations;

namespace BookAppServer.Dto.UserDto
{
    public class UserForLogin
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; init; }
    }
}
