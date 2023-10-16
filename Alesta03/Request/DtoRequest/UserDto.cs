using System.ComponentModel.DataAnnotations;

namespace Alesta03.Request.DtoRequest
{
    public class UserDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "Lütfen en az altı karakterli bir şifre oluşturunuz!")]
        public required string Password { get; set; } = string.Empty;

 
    }
}
