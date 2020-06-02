using System.ComponentModel.DataAnnotations;

namespace HWP_backend.Models.DTO.Users
{
    public class AuthenticateModelDTO
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}