using System.ComponentModel.DataAnnotations;

namespace HWP_backend.Models.DTO.Users
{
    public class RegisterModelDTO
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }

        public string Role { get; set; }
    }
}